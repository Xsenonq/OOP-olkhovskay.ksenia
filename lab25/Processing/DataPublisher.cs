using System;

namespace lab25.Processing
{
    public class DataPublisher
    {
        public event Action<string>? DataProcessed;

        public void PublishDataProcessed(string processedData)
        {
            DataProcessed?.Invoke(processedData);
        }
    }
}
