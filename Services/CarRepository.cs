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
        if (!string.IsNullOrEmpty(search))
        {
            asQueryable = asQueryable.Where(x => x.Name.Contains(search));
        }

        asQueryable = asQueryable.OrderBy(x => x.Name);
        var result = await asQueryable.ToListAsync();
        return mapper.Map<List<CarViewModel>>(result);
    }

    public async Task Create(Car car)
    {
        context.Car.Add(car);
        await context.SaveChangesAsync();
    }

    public async Task Update(Car car,string userId)
    {
        //TODO : Interceptor
        car.ModifiedDateTime = DateTime.Now;
        car.Version += 1;
        car.ModifiedBy = userId;
        context.Update(car);
        await context.SaveChangesAsync();
    }
}