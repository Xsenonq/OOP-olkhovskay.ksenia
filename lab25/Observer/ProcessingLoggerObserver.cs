using lab25.Logging;
using lab25.Processing;

namespace lab25.Observer
{
    public class ProcessingLoggerObserver
    {
        public void Subscribe(DataPublisher publisher)
        {
            publisher.DataProcessed += OnDataProcessed;
        }

        private void OnDataProcessed(string data)
        {
            LoggerManager.Instance.Log($"Data processed: {data}");
        }
    }
}
