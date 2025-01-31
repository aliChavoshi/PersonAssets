using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Models.Person;

namespace PersonAssets.Controllers;

public class PersonController(ApplicationDbContext context) : Controller
{
    // GET: Person
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await context.Person.ToListAsync(cancellationToken: cancellationToken));
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
        return View();
    }

    // POST: Person/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PersonViewModel model)
    {
        if (ModelState.IsValid)
        {
            var person = new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalCode = model.NationalCode
            };
            context.Add(person);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: Person/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var person = await context.Person.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        return View(person);
    }

    // POST: Person/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,NationalCode,Id")] Person person)
    {
        if (id != person.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(person);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.Id))
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

        return View(person);
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
            context.Person.Remove(person);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PersonExists(int id)
    {
        return context.Person.Any(e => e.Id == id);
    }
}