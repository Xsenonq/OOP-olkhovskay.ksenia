class Program
{
    static void Main()
    {
        IOrderValidator validator = new OrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        OrderService orderService = new OrderService(
            validator,
            repository,
            emailService
        );

        // Валідне замовлення
        Order validOrder = new Order(1, "Ivan", 1000);
        orderService.ProcessOrder(validOrder);

        // Невалідне замовлення
        Order invalidOrder = new Order(2, "Olena", -50);
        orderService.ProcessOrder(invalidOrder);
    }
}
