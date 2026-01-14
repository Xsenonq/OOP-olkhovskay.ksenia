public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        // Валідація
        if (order.TotalAmount <= 0)
        {
            Console.WriteLine(" Order validation failed");
            order.Status = OrderStatus.Cancelled;
            return;
        }

        Console.WriteLine(" Order validated");

        // Збереження
        Console.WriteLine(" Order saved to database");

        // Email
        Console.WriteLine($" Email sent to {order.CustomerName}");

        // Оновлення статусу
        order.Status = OrderStatus.Processed;
        Console.WriteLine(" Order processed");
    }
}