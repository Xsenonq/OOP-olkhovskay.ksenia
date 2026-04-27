using IndependentWork19.Services;

namespace IndependentWork19.Loggers;

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        string id = IdService.Instance.GenerateId();
        Console.WriteLine($"[FILE][{id}] {message}");
    }
}