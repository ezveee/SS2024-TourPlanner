using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Logging;
public interface ILogger
{
	void Info(string message);
	void Debug(string message);
	void Error(string message);
	void Fatal(string message);
	void Warn(string message);
}
