using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace TechOnIt.Application.Common.Frameworks.Middlewares;

public class ApiExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ApiExceptionHandlerMiddleware> logger;
    private readonly IWebHostEnvironment hostEnvironment;

    public ApiExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ApiExceptionHandlerMiddleware> logger,
        IWebHostEnvironment hostEnvironment)
    {
        this.next = next;
        this.logger = logger;
        this.hostEnvironment = hostEnvironment;
    }

    public async Task Invoke(HttpContext context)
    {
        string message = string.Empty;
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;

        try
        {
            await next(context);
        }
        catch (AppException exception)
        {
            logger.LogError(exception, exception.Message);
            httpStatusCode = exception.HttpStatusCode;
            apiStatusCode = exception.ApiStatusCode;

            if (hostEnvironment.IsDevelopment())
            {
                IDictionary<string, string> dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace ?? string.Empty,
                };
                if (exception.InnerException is not null)
                {
                    // add ToString() for : //https://stackoverflow.com/questions/5928976/what-is-the-proper-way-to-display-the-full-innerexception
                    dic.Add("InnerException.Exception", exception.InnerException.Message.ToString());
                    dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace ?? string.Empty);
                }
                if (exception.AdditionalData is not null)
                    dic.Add("AdditionalData", JsonSerializer.Serialize(exception.AdditionalData));

                message = JsonSerializer.Serialize(dic);
            }
            else
            {
                message = exception.Message;
            }

            await WriteToResponseAsync();
        }
        catch (SecurityTokenExpiredException exception)
        {
            logger.LogError(exception, exception.Message);
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }
        catch (UnauthorizedAccessException exception)
        {
            logger.LogError(exception, exception.Message);
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            logger.LogError(exception, exception.Message);

            var entityProprttyValues = exception.Entries.Single().GetDatabaseValues();
            if (entityProprttyValues is null)
                message = "the data being updated has been deleted by an other user !";
            else
                message = "the data being updated has already been updated by an other user !";

            await WriteToResponseAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);

            if (hostEnvironment.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace ?? string.Empty,
                };

                message = JsonSerializer.Serialize(dic);
            }
            await WriteToResponseAsync();
        }

        // local functions : https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions

        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted)
                throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

            var result = new ApiResult(apiStatusCode, new List<string> { message });
            var json = JsonSerializer.Serialize(result);

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
            await Task.CompletedTask;
        }

        void SetUnAuthorizeResponse(Exception exception)
        {
            if (hostEnvironment.IsDevelopment())
            {
                IDictionary<string, string> dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace ?? string.Empty
                };

                if (exception is SecurityTokenExpiredException tokenException)
                    dic.Add("Expires", tokenException.Expires.ToString());

                message = JsonSerializer.Serialize(dic);
            }
        }
    }
}