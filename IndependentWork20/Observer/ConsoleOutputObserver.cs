using System;

namespace IndependentWork20.Observer;

public class ConsoleOutputObserver
{
    public void Subscribe(DataPublisher publisher)
    {
        publisher.DataProcessed += OnDataProcessed;
    }

    private void OnDataProcessed(string data)
    {
        Console.WriteLine($"[Console] Received: {data}");
    }
}