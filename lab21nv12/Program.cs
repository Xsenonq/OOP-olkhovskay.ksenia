using lab21nv12.Factory;
using lab21nv12.Services;
using lab21nv12.Strategies;

namespace lab21nv12
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Тип доставки (Standard / Express / International / Night):");
            string type = Console.ReadLine();

            Console.WriteLine("Відстань (км):");
            decimal distance = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Вага (кг):");
            decimal weight = decimal.Parse(Console.ReadLine());

            try
            {
                IShippingStrategy strategy =
                    ShippingStrategyFactory.CreateStrategy(type);

                DeliveryService service = new DeliveryService();
                decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);

                Console.WriteLine($"Вартість доставки: {cost} грн");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
