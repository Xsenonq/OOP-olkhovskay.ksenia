using IndependentWork19.Services;

namespace IndependentWork19.Loggers;

public class DatabaseLogger : ILogger
{
    public void Log(string message)
    {
        string id = IdService.Instance.GenerateId();
        Console.WriteLine($"[DB][{id}] {message}");
    }
}