namespace lab25.Logging
{
    public class LoggerManager
    {
        private static LoggerManager? _instance;
        private ILogger _logger;

        private LoggerManager(LoggerFactory factory)
        {
            _logger = factory.CreateLogger();
        }

        public static LoggerManager Initialize(LoggerFactory factory)
        {
            _instance ??= new LoggerManager(factory);
            return _instance;
        }

        public static LoggerManager Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("LoggerManager not initialized.");
                return _instance;
            }
        }

        public void ChangeFactory(LoggerFactory factory)
        {
            _logger = factory.CreateLogger();
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }
}
