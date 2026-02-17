namespace lab25.Processing
{
    public class EncryptDataStrategy : IDataProcessorStrategy
    {
        public string StrategyName => "Encryption";

        public string Process(string data)
        {
            return $"Encrypted({data})";
        }
    }
}
