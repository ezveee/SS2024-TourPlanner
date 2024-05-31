using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using System.Diagnostics;


namespace UI.Logging;

public static class LoggerFactory
{
	public static ILogger GetLogger()
	{
		StackTrace stackTrace = new(1, false); //Captures 1 frame, false for not collecting information about the file
		var type = stackTrace.GetFrame(1).GetMethod().DeclaringType;
		return Logger.CreateLogger("log4net.config", type.FullName);
	}
}

