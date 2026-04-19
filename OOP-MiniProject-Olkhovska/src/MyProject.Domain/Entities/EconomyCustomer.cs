public class EconomyCustomer : Customer
{
    public EconomyCustomer(string name) : base(name) { }

    public override decimal GetDiscount()
    {
        return 0.05m;
    }
}