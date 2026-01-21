using System;

namespace IndependentWork16.BadExample
{
    public class WeatherForecastServiceBad
    {
        public void ProcessWeather()
        {
            Console.WriteLine("Отримання даних з API...");
            string apiData = "{ temp: 10, city: 'Kharkiv' }";

            Console.WriteLine("Парсинг даних...");
            string parsedData = "Kharkiv: 10°C";

            Console.WriteLine("Збереження даних...");
            Console.WriteLine("Дані збережено в БД");

            Console.WriteLine("Відображення прогнозу...");
            Console.WriteLine(parsedData);
        }
    }
}
