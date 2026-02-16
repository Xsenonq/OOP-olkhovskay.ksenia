using System.Collections.Generic;

namespace lab24.Observer
{
    public class HistoryLoggerObserver
    {
        private readonly List<string> _history = new();

        public void Subscribe(ResultPublisher publisher)
        {
            publisher.ResultCalculated += OnResultCalculated;
        }

        private void OnResultCalculated(double result, string operationName)
        {
            _history.Add($"Operation: {operationName}, Result: {result}");
        }

        public List<string> GetHistory()
        {
            return _history;
        }
    }
}
