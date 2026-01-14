public class InMemoryOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine("Order saved to in-memory database");
    }

    public Order GetById(int id)
    {
        Console.WriteLine($" Get order with id {id}");
        return null;
    }
}
