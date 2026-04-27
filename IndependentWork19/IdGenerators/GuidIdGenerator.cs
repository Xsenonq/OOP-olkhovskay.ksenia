using System;

namespace IndependentWork19.IdGenerators;

public class GuidIdGenerator : IIdGenerator
{
    public string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }
}