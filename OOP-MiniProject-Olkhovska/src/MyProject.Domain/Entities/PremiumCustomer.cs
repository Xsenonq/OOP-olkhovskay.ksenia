public class PremiumCustomer : Customer
{
    public PremiumCustomer(string name) : base(name) { }

    public override decimal GetDiscount()
    {
        return 0.2m; // 20% discount
    }
}