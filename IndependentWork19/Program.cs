using IndependentWork19.IdFactories;
using IndependentWork19.LoggerFactories;
using IndependentWork19.Managers;
using IndependentWork19.Services;

class Program
{
    static void Main(string[] args)
    {
        var idService = IdService.Instance;
        var logger = LoggerManager.Instance;

        idService.SetFactory(new GuidIdGeneratorFactory());
        logger.SetLoggerFactory(new ConsoleLoggerFactory());

        logger.Log("User created");
        logger.Log("User logged in");

        Console.WriteLine();

        idService.SetFactory(new SequentialIdGeneratorFactory());
        logger.SetLoggerFactory(new FileLoggerFactory());

        logger.Log("Order created");
        logger.Log("Order paid");

        Console.WriteLine();

        idService.SetFactory(new RandomIdGeneratorFactory());
        logger.SetLoggerFactory(new DatabaseLoggerFactory());

        logger.Log("Data saved");
        logger.Log("Data updated");
    }
}