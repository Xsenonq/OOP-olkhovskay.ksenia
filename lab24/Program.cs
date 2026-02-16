using System;
using lab24.Core;
using lab24.Strategies;
using lab24.Observer;

namespace lab24
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new ResultPublisher();

            var consoleObserver = new ConsoleLoggerObserver();
            var historyObserver = new HistoryLoggerObserver();
            var thresholdObserver = new ThresholdNotifierObserver(50);

            consoleObserver.Subscribe(publisher);
            historyObserver.Subscribe(publisher);
            thresholdObserver.Subscribe(publisher);

            var processor = new NumericProcessor(new SquareOperationStrategy());

            double[] numbers = { 4, 5, 9 };

            foreach (var number in numbers)
            {
                double result = processor.Process(number);
                publisher.PublishResult(result, processor.CurrentOperationName);
            }

            processor.SetStrategy(new CubeOperationStrategy());

            foreach (var number in numbers)
            {
                double result = processor.Process(number);
                publisher.PublishResult(result, processor.CurrentOperationName);
            }

            processor.SetStrategy(new SquareRootOperationStrategy());

            foreach (var number in numbers)
            {
                double result = processor.Process(number);
                publisher.PublishResult(result, processor.CurrentOperationName);
            }

            Console.WriteLine("\n--- History ---");
            foreach (var record in historyObserver.GetHistory())
            {
                Console.WriteLine(record);
            }
        }
    }
}
