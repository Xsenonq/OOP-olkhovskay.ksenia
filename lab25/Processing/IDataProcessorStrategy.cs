namespace lab25.Processing
{
    public interface IDataProcessorStrategy
    {
        string Process(string data);
        string StrategyName { get; }
    }
}
