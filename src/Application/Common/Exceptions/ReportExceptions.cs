namespace iot.Application.Common.Exceptions;

public class ReportExceptions : Exception
{
	#region properties
	public Guid? UserId { get; set; }
	#endregion

	public ReportExceptions()
	{

	}

	public ReportExceptions(string message):base(message)
	{

	}

	public ReportExceptions(string message,Exception innerException)
		:base(message,innerException)
	{

	}

	public ReportExceptions(string message, Guid? userId)
		:base(message)
	{
		UserId = userId;
	}

}
