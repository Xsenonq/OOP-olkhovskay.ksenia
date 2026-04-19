public class Rental
{
    public Guid Id { get; }
    public Car Car { get; }
    public Customer Customer { get; }
    public DateTime StartDate { get; }

    public Rental(Car car, Customer customer)
    {
        Car = car ?? throw new ArgumentNullException();
        Customer = customer ?? throw new ArgumentNullException();

        Id = Guid.NewGuid();
        StartDate = DateTime.Now;
    }
}