using IndependentWork19.IdFactories;

namespace IndependentWork19.Services;

public class IdService
{
    private static IdService _instance;
    private static readonly object _lock = new object();

    private IdGeneratorFactory _factory;

    private IdService() { }

    public static IdService Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new IdService();
                }
            }
            return _instance;
        }
    }

    public void SetFactory(IdGeneratorFactory factory)
    {
        _factory = factory;
    }

    public string GenerateId()
    {
        if (_factory == null)
            throw new Exception("ID Factory not set!");

        return _factory.Generate();
    }
}