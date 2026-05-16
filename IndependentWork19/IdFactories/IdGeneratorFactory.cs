using IndependentWork19.IdGenerators;

namespace IndependentWork19.IdFactories;

public abstract class IdGeneratorFactory
{
    protected abstract IIdGenerator CreateGenerator();

    public string Generate()
    {
        return CreateGenerator().GenerateId();
    }
}