using IndependentWork19.LoggerFactories;

namespace IndependentWork19.Managers;

public class LoggerManager
{
    private static LoggerManager _instance;
    private static readonly object _lock = new object();

    private LoggerFactory _factory;

    private LoggerManager() { }

    public static LoggerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new LoggerManager();
                }
            }
            return _instance;
        }
    }

    public void SetLoggerFactory(LoggerFactory factory)
    {
        _factory = factory;
    }

    public void Log(string message)
    {
        if (_factory == null)
            throw new Exception("Logger factory not set!");

        _factory.LogMessage(message);
    }
}