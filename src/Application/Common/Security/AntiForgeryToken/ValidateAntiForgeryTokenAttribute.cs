using System.Net;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TechOnIt.Application.Common.Security.AntiForgeryToken;

// https://stackoverflow.com/questions/11476883/web-api-and-validateantiforgerytoken

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
{
    public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
        HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    {
        try
        {
            AntiForgery.Validate();
        }
        catch
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                RequestMessage = actionContext.ControllerContext.Request
            };
            return FromResult(actionContext.Response);
        }
        return continuation();
    }

    private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
    {
        var source = new TaskCompletionSource<HttpResponseMessage>();
        source.SetResult(result);
        return source.Task;
    }
}
