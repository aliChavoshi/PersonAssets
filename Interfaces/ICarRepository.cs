﻿using PersonAssets.Data.Entity;
using PersonAssets.Models.Car;

namespace PersonAssets.Interfaces;

public interface ICarRepository
{
    Task<List<CarViewModel>> GetAllCars(string search);
    Task Create(Car car);
    Task Update(Car car, string userId);
    Task<Car> GetCarById(int carId);
    Task AddOwnerOfCar(int carId,int personId);
    Task<List<AddOwnerViewModel>> GetOwners(int carId);
    Task<bool> ExistOwnerInCar(int carId, int ownerId);
    Task<int> OwnersOfCar(int carId);
    Task<List<ConfirmPersonCarViewModel>> GetUnConfirmedPersonCar();
    Task<bool> ApprovePersonCar(int carId,int personId);
}