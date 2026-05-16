using IndependentWork19.Loggers;

namespace IndependentWork19.LoggerFactories;

public class ConsoleLoggerFactory : LoggerFactory
{
    protected override ILogger CreateLogger()
    {
        return new ConsoleLogger();
    }
}