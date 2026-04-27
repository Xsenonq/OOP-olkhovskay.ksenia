using IndependentWork19.IdGenerators;

namespace IndependentWork19.IdFactories;

public class RandomIdGeneratorFactory : IdGeneratorFactory
{
    protected override IIdGenerator CreateGenerator()
    {
        return new RandomIdGenerator();
    }
}