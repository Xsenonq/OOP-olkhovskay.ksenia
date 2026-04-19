using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.Application.Services;

public class RentalService
{
    private readonly ICarRepository _carRepository;

    public RentalService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public void RentCar(string customerName, Guid carId)
    {
        var car = _carRepository.GetById(carId);

        if (car == null)
            throw new Exception("Car not found");

        car.Rent();

        Console.WriteLine($"Customer {customerName} rented {car.Model}");
    }
}