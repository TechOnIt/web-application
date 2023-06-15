namespace TechOnIt.Application.Common.Exceptions;

public class ReportExceptions : AppException
{
    #region Ctors
    public ReportExceptions()
    {

    }

    public ReportExceptions(string message) : base(message)
    {

    }

    public ReportExceptions(string message, Exception innerException)
        : base(message, innerException)
    {

    }

    public ReportExceptions(string message, Guid? userId)
        : base(message)
    {
        UserId = userId;
    }
    #endregion

    public Guid? UserId { get; set; }
}