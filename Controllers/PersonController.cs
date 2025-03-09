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
using PersonAssets.Interfaces;
using PersonAssets.Models.Person;
using PersonAssets.Services;

namespace PersonAssets.Controllers;

public class PersonController(IMapper mapper, IPersonRepository personRepository)
    : CommonAuthorizeController
{
    // GET: Person
    public async Task<IActionResult> Index(string searchString)
    {
        ViewBag.search = searchString;
        // ViewData["search"] = searchString;
        // TempData["success"] = true;
        return View(await personRepository.GetAllPersons(searchString));
    }

    public async Task<IActionResult> IndexOfDeletedUser()
    {
        return View(await personRepository.GetDeletedUsers());
    }

    // GET: Person/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(); //empty
    }

    // POST: Person/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePersonViewModel model)
    {
        foreach (var key in ModelState.Keys)
        {
            var value = ModelState[key]; // key value
            foreach (var error in value!.Errors)
            {
                ModelState.AddModelError(key, error.ErrorMessage);
                return View(model);
            }
        }

        if (await personRepository.IsAnyNationalCode(model.NationalCode))
        {
            ModelState.AddModelError(nameof(model.NationalCode), "کد ملی استفاده شده نا معتبر میباشد");
            return View(model);
        }

        if (ModelState.IsValid)
        {
            var person = mapper.Map<Person>(model);
            await personRepository.Create(person);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: Person/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var person = await personRepository.GetPersonById(id); //Person
        if (person == null) return NotFound();

        return View(mapper.Map<EditPersonViewModel>(person));
    }

    // POST: Person/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditPersonViewModel model)
    {
        var person = await personRepository.GetPersonById(model.Id);
        if (person!.NationalCode != model.NationalCode && await personRepository.IsAnyNationalCode(model.NationalCode))
        {
            ModelState.AddModelError(nameof(model.NationalCode), "کد ملی استفاده شده نا معتبر میباشد");
            return View(model);
        }

        if (ModelState.IsValid)
        {
            mapper.Map(model, person);
            await personRepository.Edit(person);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: Person/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var person = await personRepository.GetPersonById(id.Value);
        // var person = await context.Person.FindAsync(id.Value);
        if (person == null)
        {
            return NotFound();
        }

        return View(person);
    }

    // POST: Person/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var person = await personRepository.GetPersonById(id);
        if (person != null)
        {
            person.IsDeleted = true;
            await personRepository.Delete(person);
        }

        return RedirectToAction(nameof(Index));
    }
}