public class ConsoleEmailService : IEmailService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"Email confirmation sent to {order.CustomerName}");
    }
}
