using lab21nv12.Strategies;

namespace lab21nv12.Strategies
{
    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight)
        {
            return (distance * 2.5m + weight * 1.0m) + 50m;
        }
    }
}
