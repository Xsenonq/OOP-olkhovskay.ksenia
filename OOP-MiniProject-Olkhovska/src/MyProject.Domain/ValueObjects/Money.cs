public class Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Money cannot be negative");

        Amount = amount;
    }
}