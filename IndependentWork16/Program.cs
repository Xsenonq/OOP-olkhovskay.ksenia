class Program
{
    static void Main()
    {
        var apiClient = new MockWeatherApiClient();
        var parser = new SimpleWeatherParser();
        var repository = new InMemoryWeatherRepository();
        var display = new ConsoleWeatherDisplay();

        var weatherService = new WeatherService(
            apiClient,
            parser,
            repository,
            display
        );

        weatherService.ProcessWeather();
    }
}
