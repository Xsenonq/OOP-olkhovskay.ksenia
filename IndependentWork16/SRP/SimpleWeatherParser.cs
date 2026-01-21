using System;

public class SimpleWeatherParser : IWeatherParser
{
    public string Parse(string rawData)
    {
        Console.WriteLine("Парсинг погодних даних");
        return "Kharkiv : 12°C";
    }
}
