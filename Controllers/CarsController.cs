using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Data.Entity;
using PersonAssets.Extension;
using PersonAssets.Interfaces;
using PersonAssets.Models.Car;

namespace PersonAssets.Controllers;

public class CarsController(
    ApplicationDbContext context,
    ICarRepository carRepository,
    IMapper mapper,
    IPersonRepository personRepository)
    : CommonAuthorizeController
{
    //1. Interface
    //2.
    // GET: Cars
    // [Authorize]
    // [AllowAnonymous]
    public async Task<IActionResult> Index(string searchString)
    {
        var model = await carRepository.GetAllCars(searchString);
        return View(model);
    }

    // GET: Cars/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var car = await context.Car
            .FirstOrDefaultAsync(m => m.Id == id);
        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }

    // GET: Cars/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Cars/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCarViewModel model)
    {
        if (ModelState.IsValid)
        {
            var car = mapper.Map<Car>(model);
            car.CreatedBy = User.GetId();
            await carRepository.Create(car);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: Cars/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var car = await context.Car.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }

    // POST: Cars/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Car model)
    {
        var car = await context.Car.FindAsync(id);
        if (car == null) return NotFound(); //404
        car.Name = model.Name;
        car.Color = model.Color;
        car.Type = model.Type;
        car.NumberPlate = model.NumberPlate;
        await carRepository.Update(car, User.GetId());
        return RedirectToAction("Index");
    }

    // GET: Cars/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var car = await context.Car
            .FirstOrDefaultAsync(m => m.Id == id);
        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }

    // POST: Cars/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var car = await context.Car.FindAsync(id);
        if (car != null)
        {
            context.Car.Remove(car);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{carId:int}")] //http:localhost:5200/carId
    public async Task<IActionResult> AddOwner(int carId)
    {
        var model = await InitialDataBeforeAddNewOwner(carId);
        return View(model);
    }

    private async Task<AddOwnerViewModel> InitialDataBeforeAddNewOwner(int carId)
    {
        ViewData["Persons"] = await personRepository.GetPersonSelectList(0); //get list
        var car = await carRepository.GetCarById(carId); // get car
        var model = mapper.Map<AddOwnerViewModel>(car); //map car=> model : AddOwnerViewModel
        model.Owners = await carRepository.GetOwners(carId); //owners

        return model;
    }

    [HttpPost("{carId:int}")] //http:localhost:5200/carId
    public async Task<IActionResult> AddOwner(AddOwnerViewModel model)
    {
        //validation
        if (await carRepository.ExistOwnerInCar(model.CarId, model.OwnerId))
        {
            ModelState.AddModelError("","برای این خودرو این شخص از قبل ثبت شده است");
            var myModel = await InitialDataBeforeAddNewOwner(model.CarId); // load data
            return View(myModel);
        }
        await carRepository.AddOwnerOfCar(model.CarId, model.OwnerId); //add new Item
        var view = await InitialDataBeforeAddNewOwner(model.CarId); // load data
        return View(view);
    }
}