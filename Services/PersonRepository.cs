using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Data.Entity;
using PersonAssets.Interfaces;

namespace PersonAssets.Services;

public class PersonRepository(ApplicationDbContext context) : IPersonRepository
{
    // private readonly ApplicationDbContext _context;
    // public PersonRepository(ApplicationDbContext context)
    // {
    //     _context = context;
    // }
    public async Task Create(Person person)
    {
        context.Person.Add(person);
        await Save();
    }

    public async Task<Person> GetPersonById(int id)
    {
        return await context.Person.FindAsync(id);
    }

    public async Task<List<Person>> GetAllPersons(string search)
    {
        var asQueryable = context.Person.AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            asQueryable = asQueryable.Where(x =>
                x.LastName.Contains(search) ||
                x.FirstName.Contains(search) ||
                x.NationalCode.Contains(search));
        }

        //OrderBy
        asQueryable = asQueryable.OrderBy(x => x.LastName);
        //TODO : Pagination
        //Run
        return await asQueryable.ToListAsync();
    }

    public async Task<List<Person>> GetDeletedUsers()
    {
        return await context.Person
            .IgnoreQueryFilters()
            .Where(x => x.IsDeleted == true)
            .ToListAsync();
    }

    public async Task<Person> Edit(Person person)
    {
        context.Person.Update(person);
        await Save();
        return person;
    }

    public async Task Delete(int id)
    {
        var person = await GetPersonById(id);
        person.IsDeleted = true;
        await Edit(person);
    }

    public async Task Delete(Person person)
    {
        person.IsDeleted = true;
        await Edit(person);
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsAnyNationalCode(string nationalCode)
    {
        return await context.Person.AnyAsync(x => x.NationalCode == nationalCode);
    }

    public async Task<SelectList> GetPersonSelectList(int personId)
    {
        var persons = await context.Person.ToListAsync();

        var items = persons.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(), // Assuming `Id` is the primary key
            Text = p.LastName + " " + p.FirstName, // Assuming `Name` is a displayable field
        }).ToList();

        return new SelectList(items, "Value", "Text", personId);
    }
}