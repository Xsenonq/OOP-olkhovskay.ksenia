using IndependentWork19.Loggers;

namespace IndependentWork19.LoggerFactories;

public class DatabaseLoggerFactory : LoggerFactory
{
    protected override ILogger CreateLogger()
    {
        return new DatabaseLogger();
    }
}