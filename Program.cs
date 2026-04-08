using lab21nv12.Factory;
using lab21nv12.Services;
using lab21nv12.Strategies;

namespace lab21nv12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Тип доставки (Standard / Express / International / Night):");
            string? type = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Тип доставки не може бути порожнім.");
                return;
            }

            Console.WriteLine("Введіть відстань (км):");
            if (!decimal.TryParse(Console.ReadLine(), out decimal distance))
            {
                Console.WriteLine("Некоректна відстань.");
                return;
            }

            Console.WriteLine("Введіть вагу (кг):");
            if (!decimal.TryParse(Console.ReadLine(), out decimal weight))
            {
                Console.WriteLine("Некоректна вага.");
                return;
            }

            try
            {
                IShippingStrategy strategy =
                    ShippingStrategyFactory.CreateStrategy(type);

                DeliveryService service = new DeliveryService();
                decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);

                Console.WriteLine($"Вартість доставки: {cost} грн");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
