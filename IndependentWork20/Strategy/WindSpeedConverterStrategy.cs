using System;

namespace IndependentWork20.Strategy;

public class WindSpeedConverterStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        if (double.TryParse(data, out double s))
        {
            return $"Wind speed: {s * 3.6} km/h";
        }
        return "Invalid wind data";
    }
}