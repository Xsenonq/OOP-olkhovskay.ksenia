using System;

public class ConsoleWeatherDisplay : IWeatherDisplay
{
    public void Show(string weatherData)
    {
        Console.WriteLine($"Прогноз погоди: {weatherData}");
    }
}
