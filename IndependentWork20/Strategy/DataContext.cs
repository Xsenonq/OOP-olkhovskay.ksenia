namespace IndependentWork20.Strategy;

public class DataContext
{
    private IDataProcessorStrategy _strategy;

    public DataContext(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public string ExecuteProcessing(string data)
    {
        return _strategy.Process(data);
    }
}