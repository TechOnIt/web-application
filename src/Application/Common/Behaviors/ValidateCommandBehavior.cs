using Microsoft.Extensions.Logging;
using System.Text;

namespace iot.Application.Common.Behaviors
{
    public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        // general command validation for all commands

        #region Constracture
        // a collection of all validation classes in project
        // https://stackoverflow.com/questions/63216905/unable-to-resolve-fluent-validation-using-mediatr-without-structuremap
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidateCommandBehavior<TRequest, TResponse>> _logger;

        public ValidateCommandBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidateCommandBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }
        #endregion

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // validate our commands in these lines of linq expression
            // what we did here ? we found our validation class related 
            // to our command - we have command on method parameter-
            // then in this linq expression we found the spcific validator 
            // for our command then we keep validation our model to find any error
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(a => a != null)
                .ToList();

            if (errors.Any()) // if we have any kind of error
            {
                #region append all errors into an instance of string builder

                var errorBuilder = new StringBuilder();
                errorBuilder.AppendLine("Invalid command, reasons: ");

                foreach (var error in errors)
                {
                    errorBuilder.AppendLine(error.ErrorMessage);
                }

                #endregion

                // throw errors as exception
                throw new Exception(errorBuilder.ToString());
            }
            else
            {
                _logger.LogInformation($"Validation of the model was done successfully : {typeof(TRequest)}");
            }

            // In any case we will continue next operation
            return next();
        }
    }
}