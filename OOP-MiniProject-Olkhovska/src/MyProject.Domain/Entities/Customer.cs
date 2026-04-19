public abstract class Customer
{
    public Guid Id { get; }
    public string Name { get; }

    protected Customer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name required");

        Id = Guid.NewGuid();
        Name = name;
    }

    public abstract decimal GetDiscount();
}