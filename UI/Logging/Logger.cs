using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using UI.Logging;


public class Logger : ILogger
{
    private readonly ILog _logger;

	public static Logger CreateLogger(string configPath, string caller)
	{
		var realPath = Path.Combine(@"..\..\..\", configPath);
		if (!File.Exists(realPath))
		{
			throw new ArgumentException("Does not exist.", nameof(configPath));
		}

		log4net.Config.XmlConfigurator.Configure(new FileInfo(realPath));
		var logger = log4net.LogManager.GetLogger(caller);  // System.Reflection.MethodBase.GetCurrentMethod().DeclaringType

		return new Logger(logger);
	}

	private Logger(log4net.ILog logger)
	{
		this._logger = logger;
	}

	public void Info(string message)
	{
		this._logger.Info(message);
	}

	public void Debug(string message)
	{
		this._logger.Debug(message);
	}

	public void Warn(string message)
	{
		this._logger.Warn(message);
	}

	public void Error(string message)
	{
		this._logger.Error(message);
	}

	public void Fatal(string message)
	{
		this._logger.Fatal(message);
	}

}