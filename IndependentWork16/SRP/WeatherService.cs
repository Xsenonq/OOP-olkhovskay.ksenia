public class WeatherService
{
    private readonly IWeatherApiClient _apiClient;
    private readonly IWeatherParser _parser;
    private readonly IWeatherRepository _repository;
    private readonly IWeatherDisplay _display;

    public WeatherService(
        IWeatherApiClient apiClient,
        IWeatherParser parser,
        IWeatherRepository repository,
        IWeatherDisplay display)
    {
        _apiClient = apiClient;
        _parser = parser;
        _repository = repository;
        _display = display;
    }

    public void ProcessWeather()
    {
        var rawData = _apiClient.GetWeatherData();
        var parsedData = _parser.Parse(rawData);
        _repository.Save(parsedData);
        _display.Show(parsedData);
    }
}
