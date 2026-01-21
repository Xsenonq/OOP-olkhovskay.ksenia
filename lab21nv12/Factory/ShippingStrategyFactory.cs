using lab21nv12.Strategies;

namespace lab21nv12.Factory
{
    public static class ShippingStrategyFactory
    {
        public static IShippingStrategy CreateStrategy(string type)
        {
            return type.ToLower() switch
            {
                "standard" => new StandardShippingStrategy(),
                "express" => new ExpressShippingStrategy(),
                "international" => new InternationalShippingStrategy(),
                "night" => new NightShippingStrategy(),
                _ => throw new ArgumentException("Невідомий тип доставки")
            };
        }
    }
}

