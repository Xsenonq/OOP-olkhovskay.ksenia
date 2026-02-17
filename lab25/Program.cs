
using System;
using lab25.Logging;
using lab25.Processing;
using lab25.Observer;

namespace lab25
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SCENARIO 1: FULL INTEGRATION");

            LoggerManager.Initialize(new ConsoleLoggerFactory());

            var context = new DataContext(new EncryptDataStrategy());
            var publisher = new DataPublisher();
            var observer = new ProcessingLoggerObserver();

            observer.Subscribe(publisher);

            var result = context.Execute("MyData");
            publisher.PublishDataProcessed(result);

            Console.WriteLine("\n SCENARIO 2: CHANGE LOGGER ");

            LoggerManager.Instance.ChangeFactory(new FileLoggerFactory());

            var result2 = context.Execute("MyDataAgain");
            publisher.PublishDataProcessed(result2);

            Console.WriteLine("Check log.txt for file logging.");

            Console.WriteLine("\nSCENARIO 3: CHANGE STRATEGY ");

            context.SetStrategy(new CompressDataStrategy());

            var result3 = context.Execute("MyDataCompressed");
            publisher.PublishDataProcessed(result3);

            Console.WriteLine("Processing completed.");
        }
    }
}
