namespace iot.Application.Common.Exceptions;

public class StructureException : AppException
{
	public StructureException()
	{

	}

	public StructureException(string message) :base(message)
	{

	}

	public StructureException(string message,Exception innerexception)
	{

	}
}
