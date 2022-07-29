using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Users
{
    public class UserCreateCommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<UserCreateCommand, Guid>
    {
        #region Constracture
        private readonly ILogger<UserCreateCommandValidationBehavior<TRequest, TResponse>> _logger;
        public UserCreateCommandValidationBehavior(ILogger<UserCreateCommandValidationBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        #endregion

        public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<Guid> next)
        {
            var validator = new UserCreateCommandValidator();
            var check = validator.Validate(request);

            if (check.IsValid)
            {
                var response = await next();
                return response;
            }
            else
            {
                _logger.LogInformation($"validation has been filed {typeof(TResponse)}");
                throw new Exception("nothing yet ...");
            }
        }
    }
}
