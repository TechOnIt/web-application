using TechOnIt.Application.Common.Enums.IdentityService;

namespace TechOnIt.Application.Common.Exceptions;

public class CommandException : AppException
{
    public IdentityCrudStatus OperationStatus { get; set; }
    public object AdditionalData { get; set; }

    public CommandException(IdentityCrudStatus statusCode, string message, Exception exception, object additionalData)
       : base(message, exception)
    {
        OperationStatus = statusCode;
        AdditionalData = additionalData;
    }

    public CommandException(IdentityCrudStatus operationStatus, string message)
        : this(operationStatus, message, null, null)
    {
    }
}