using System;

public class InMemoryWeatherRepository : IWeatherRepository
{
    public void Save(string weatherData)
    {
        Console.WriteLine($"Збережено в памʼяті: {weatherData}");
    }
}
