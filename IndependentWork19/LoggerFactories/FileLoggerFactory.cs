using IndependentWork19.Loggers;

namespace IndependentWork19.LoggerFactories;

public class FileLoggerFactory : LoggerFactory
{
    protected override ILogger CreateLogger()
    {
        return new FileLogger();
    }
}