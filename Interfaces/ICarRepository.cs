using PersonAssets.Data.Entity;
using PersonAssets.Models.Car;

namespace PersonAssets.Interfaces;

public interface ICarRepository
{
    Task<List<CarViewModel>> GetAllCars(string search);
    Task Create(Car car);
}