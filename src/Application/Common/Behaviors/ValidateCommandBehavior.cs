using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Common.Behaviors
{
    public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : MediatR.IRequest<TResponse>
    {
        // general command validation for all commands

        #region Constracture
        // a collection of all validation classes in project
        private readonly IList<IValidator<TRequest>> _validators;
        public ValidateCommandBehavior(IList<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        #endregion

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
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

            // In any case we will continue next operation
            return next();
        }
    }
}
