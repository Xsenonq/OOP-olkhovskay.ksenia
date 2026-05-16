using MyProject.Domain.Entities;
using Xunit;

public class CustomerTests
{
    [Fact]
    public void ShouldCreateCustomerWithValidName()
    {
        var customer = new EconomyCustomer("John");

        Assert.Equal("John", customer.Name);
    }

    [Fact]
    public void ShouldThrowExceptionForEmptyName()
    {
        Assert.Throws<ArgumentException>(() => new EconomyCustomer(""));
    }

    [Fact]
    public void ShouldReturnDifferentDiscounts()
    {
        var premium = new PremiumCustomer("Alex");
        var economy = new EconomyCustomer("Bob");

        Assert.True(premium.GetDiscount() > economy.GetDiscount());
    }
}