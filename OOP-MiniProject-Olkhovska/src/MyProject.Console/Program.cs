using MyProject.Application.Services;
using MyProject.Domain.Entities;
using MyProject.Infrastructure.Repositories;

var repo = new InMemoryCarRepository();
var service = new RentalService(repo);

var car = new Car("BMW M3");
repo.Add(car);

Console.WriteLine("CAR RENTAL SYSTEM");
Console.WriteLine("Enter your name:");
var name = Console.ReadLine();

Console.WriteLine("Available cars:");
foreach (var c in repo.GetAll())
{
    Console.WriteLine($"{c.Id} - {c.Model} - Available: {c.IsAvailable}");
}

Console.WriteLine("Enter car ID:");
var input = Console.ReadLine();

try
{
    service.RentCar(name!, Guid.Parse(input!));
    Console.WriteLine("Car rented successfully!");
}
catch (Exception ex)
{
    Console.WriteLine("-" + ex.Message);
}