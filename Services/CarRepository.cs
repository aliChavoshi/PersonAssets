﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Data.Entity;
using PersonAssets.Interfaces;
using PersonAssets.Models.Car;

namespace PersonAssets.Services;

public class CarRepository(ApplicationDbContext context, IMapper mapper) : ICarRepository
{
    public async Task<List<CarViewModel>> GetAllCars(string search)
    {
        var asQueryable = context.Car
            .Include(x => x.CreateUser)
            .Include(x => x.ModifyUser)
            .AsQueryable();
        //Filters
        if (!string.IsNullOrEmpty(search))
        {
            asQueryable = asQueryable.Where(x => x.Name.Contains(search));
        }

        //OrderBy
        asQueryable = asQueryable.OrderBy(x => x.Name);
        //ToList
        var result = await asQueryable.ToListAsync();
        //Return Model
        return mapper.Map<List<CarViewModel>>(result);
    }

    public async Task Create(Car car)
    {
        context.Car.Add(car);
        await context.SaveChangesAsync();
    }

    public async Task Update(Car car, string userId)
    {
        //TODO : Interceptor
        car.ModifiedDateTime = DateTime.Now;
        car.Version += 1;
        car.ModifiedBy = userId;
        context.Update(car);
        await context.SaveChangesAsync();
    }

    public async Task<Car> GetCarById(int carId)
    {
        return await context.Car.FindAsync(carId);
    }

    public async Task AddOwnerOfCar(int carId, int personId)
    {
        var personCar = new PersonCar
        {
            CarId = carId,
            PersonId = personId
        };
        context.Add(personCar);
        await context.SaveChangesAsync();
    }

    public async Task<List<AddOwnerViewModel>> GetOwners(int carId)
    {
        var personCars = await context.PersonCars.Where(x => x.CarId == carId)
            .Include(p => p.Person)
            .ToListAsync();
        //
        var list = new List<AddOwnerViewModel>();
        foreach (var personCar in personCars)
        {
            list.Add(new AddOwnerViewModel()
            {
                CarId = personCar.CarId,
                OwnerId = personCar.PersonId,
                OwnerName = personCar.Person.LastName + " " + personCar.Person.FirstName + "--" + personCar.Person.NationalCode
            });
        }

        return list;
    }
}