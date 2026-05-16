using MyProject.Domain.Entities;
using Xunit;

public class CarTests
{
    [Fact]
    public void ShouldCreateCarWithValidModel()
    {
        var car = new Car("BMW");

        Assert.Equal("BMW", car.Model);
        Assert.True(car.IsAvailable);
    }

    [Fact]
    public void ShouldRentCarSuccessfully()
    {
        var car = new Car("Audi");

        car.Rent();

        Assert.False(car.IsAvailable);
    }

    [Fact]
    public void ShouldReturnCarSuccessfully()
    {
        var car = new Car("Tesla");

        car.Rent();
        car.Return();

        Assert.True(car.IsAvailable);
    }

    [Fact]
    public void ShouldNotAllowDoubleRent()
    {
        var car = new Car("BMW");

        car.Rent();

        Assert.Throws<InvalidOperationException>(() => car.Rent());
    }

    [Fact]
    public void ShouldThrowExceptionForInvalidModel()
    {
        Assert.Throws<ArgumentException>(() => new Car(""));
    }
}