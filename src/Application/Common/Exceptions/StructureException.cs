using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Common.Exceptions;

public class StructureException : Exception
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
