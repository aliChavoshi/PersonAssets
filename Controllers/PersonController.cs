using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Models.Person;

namespace PersonAssets.Controllers;

public class PersonController(ApplicationDbContext context, IMapper mapper) : Controller
{
    // GET: Person
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await context.Person.ToListAsync(cancellationToken: cancellationToken));
    }

    public async Task<IActionResult> IndexOfDeletedUser()
    {
        return View(await context.Person.IgnoreQueryFilters().Where(x=>x.IsDeleted == true).ToListAsync());
    }

    // GET: Person/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var person = await context.Person
            .FirstOrDefaultAsync(m => m.Id == id);
        if (person == null)
        {
            return NotFound();
        }

        return View(person);
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
        if (ModelState.IsValid)
        {
            var person = mapper.Map<Person>(model);
            context.Add(person);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: Person/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var person = await context.Person.FindAsync(id); //Person
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
        if (ModelState.IsValid)
        {
            var person = await context.Person.FindAsync(model.Id);
            mapper.Map(model, person);
            if (person != null) context.Person.Update(person);
            await context.SaveChangesAsync();
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

        var person = await context.Person
            .FirstOrDefaultAsync(m => m.Id == id);
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
        var person = await context.Person.FindAsync(id);
        if (person != null)
        {
            person.IsDeleted = true;
            context.Person.Update(person);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PersonExists(int id)
    {
        return context.Person.Any(e => e.Id == id);
    }
}