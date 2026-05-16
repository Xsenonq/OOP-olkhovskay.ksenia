namespace MyProject.Domain.Interfaces;

using MyProject.Domain.Entities;

public interface ICarRepository
{
    void Add(Car car);
    Car? GetById(Guid id);
    List<Car> GetAll();
}