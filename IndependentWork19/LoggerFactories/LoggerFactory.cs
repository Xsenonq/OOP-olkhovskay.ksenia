using IndependentWork19.Loggers;

namespace IndependentWork19.LoggerFactories;

public abstract class LoggerFactory
{
    protected abstract ILogger CreateLogger();

    public void LogMessage(string message)
    {
        CreateLogger().Log(message);
    }
}