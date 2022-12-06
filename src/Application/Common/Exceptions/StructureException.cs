namespace iot.Application.Common.Exceptions;

public class StructureException : AppException
{
    #region Ctors
    public StructureException()
    {
    }

    public StructureException(string message) : base(message)
    {
    }

    public StructureException(string message, Exception innerexception)
    {
    }
    #endregion
}