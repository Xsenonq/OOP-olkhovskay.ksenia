using IndependentWork19.IdGenerators;

namespace IndependentWork19.IdFactories;

public class SequentialIdGeneratorFactory : IdGeneratorFactory
{
    protected override IIdGenerator CreateGenerator()
    {
        return new SequentialIdGenerator();
    }
}