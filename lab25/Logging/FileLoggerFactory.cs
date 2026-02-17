namespace lab25.Logging
{
    public class FileLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger()
        {
            return new FileLogger();
        }
    }
}
