namespace lab25.Processing
{
    public class CompressDataStrategy : IDataProcessorStrategy
    {
        public string StrategyName => "Compression";

        public string Process(string data)
        {
            return $"Compressed({data})";
        }
    }
}
