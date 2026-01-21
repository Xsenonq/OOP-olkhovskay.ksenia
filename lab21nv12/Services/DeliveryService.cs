using lab21nv12.Strategies;

namespace lab21nv12.Services
{
    public class DeliveryService
    {
        public decimal CalculateDeliveryCost(
            decimal distance,
            decimal weight,
            IShippingStrategy strategy)
        {
            return strategy.CalculateCost(distance, weight);
        }
    }
}
