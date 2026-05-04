using System;

namespace IndependentWork20.Strategy;

public class CelsiusToFahrenheitStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        if (double.TryParse(data, out double c))
        {
            return $"Celsius -> Fahrenheit: {(c * 9 / 5) + 32} F";
        }
        return "Invalid temperature data";
    }
}