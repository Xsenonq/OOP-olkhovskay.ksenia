namespace lab21nv12.Strategies
{
    public interface IShippingStrategy
    {
        decimal CalculateCost(decimal distance, decimal weight);
    }
}
