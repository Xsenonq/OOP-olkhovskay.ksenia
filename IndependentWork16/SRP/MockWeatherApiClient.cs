using System;

public class MockWeatherApiClient : IWeatherApiClient
{
    public string GetWeatherData()
    {
        Console.WriteLine("Отримання даних з Weather API");
        return "{ temp: 12, city: 'Kharkiv' }";
    }
}
