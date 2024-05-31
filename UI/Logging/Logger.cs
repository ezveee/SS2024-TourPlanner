using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using UI.Logging;


public class Logger : ILogger
{
    private readonly ILog _logger;

    public Logger()
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        _logger = LogManager.GetLogger(typeof(Logger));
    }

    public void LogInfo(string message)
    {
        _logger.Info(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warn(message);
    }

    public void LogError(string message, Exception exception = null)
    {
        if (exception == null)
        {
            _logger.Error(message);
        }
        else
        {
            _logger.Error(message, exception);
        }
    }

    public void LogDebug(string message)
    {
        _logger.Debug(message);
    }
}