using System;

namespace IndependentWork19.IdGenerators;

public class RandomIdGenerator : IIdGenerator
{
    private Random _random = new Random();

    public string GenerateId()
    {
        return $"RAND-{_random.Next(1000, 9999)}";
    }
}