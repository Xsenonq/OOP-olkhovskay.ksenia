using System;

namespace IndependentWork20.Observer;

public class WeatherDatabaseObserver
{
    public void Subscribe(DataPublisher publisher)
    {
        publisher.DataProcessed += OnDataProcessed;
    }

    private void OnDataProcessed(string data)
    {
        Console.WriteLine($"[Database] Saved: {data}");
    }
}