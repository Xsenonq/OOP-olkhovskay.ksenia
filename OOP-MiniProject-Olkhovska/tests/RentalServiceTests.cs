using MyProject.Application.Services;
using MyProject.Domain.Entities;
using MyProject.Infrastructure.Repositories;
using Xunit;

public class RentalServiceTests
{
    [Fact]
    public void ShouldRentCarThroughService()
    {
        var repo = new InMemoryCarRepository();
        var service = new RentalService(repo);

        var car = new Car("BMW");
        repo.Add(car);

        service.RentCar("Ksenia", car.Id);

        Assert.False(car.IsAvailable);
    }

    [Fact]
    public void ShouldThrowIfCarNotFound()
    {
        var repo = new InMemoryCarRepository();
        var service = new RentalService(repo);

        Assert.Throws<Exception>(() =>
            service.RentCar("User", Guid.NewGuid()));
    }
}