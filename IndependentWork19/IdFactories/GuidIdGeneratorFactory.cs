using IndependentWork19.IdGenerators;

namespace IndependentWork19.IdFactories;

public class GuidIdGeneratorFactory : IdGeneratorFactory
{
    protected override IIdGenerator CreateGenerator()
    {
        return new GuidIdGenerator();
    }
}