using System;

namespace IndependentWork20.Strategy;

public class FahrenheitToCelsiusStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        if (double.TryParse(data, out double f))
        {
            return $"Fahrenheit -> Celsius: {(f - 32) * 5 / 9} C";
        }
        return "Invalid temperature data";
    }
}