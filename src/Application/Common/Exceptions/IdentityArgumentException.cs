namespace TechOnIt.Application.Common.Exceptions;

public class IdentityArgumentException : Exception
{
    public IdentityArgumentException()
    {
    }

    public IdentityArgumentException(string message) : base(message)
    {
    }
}