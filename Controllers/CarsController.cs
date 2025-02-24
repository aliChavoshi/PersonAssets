using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Data.Entity;
using PersonAssets.Extension;
using PersonAssets.Interfaces;
using PersonAssets.Models.Car;

namespace PersonAssets.Controllers;

public class CarsController(ApplicationDbContext context,ICarRepository carRepository,IMapper mapper) : Controller
{
    //1. Interface
    //2.
    // GET: Cars
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
    public async Task<IActionResult> Edit(int id, [Bind("Color,Type,Name,NumberPlate,Id,IsDeleted")] Car car)
    {
        if (id != car.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(car);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(car);
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

    private bool CarExists(int id)
    {
        return context.Car.Any(e => e.Id == id);
    }
}