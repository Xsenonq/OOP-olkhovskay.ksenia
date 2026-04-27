using IndependentWork19.Services;

namespace IndependentWork19.Loggers;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        string id = IdService.Instance.GenerateId();
        Console.WriteLine($"[CONSOLE][{id}] {message}");
    }
}