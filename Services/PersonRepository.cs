using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Interfaces;

namespace PersonAssets.Services;

public class PersonRepository(ApplicationDbContext context) : IPersonRepository
{
    public async  Task Create(Person person)
    {
        context.Person.Add(person);
        await Save();
    }

    public async Task<List<Person>> GetAllPersons()
    {
        return await context.Person.ToListAsync();
    }

    public Task<Person> Edit(Person person)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Person person)
    {
        throw new NotImplementedException();
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsAnyNationalCode(string nationalCode)
    {
        return await context.Person.AnyAsync(x => x.NationalCode == nationalCode);
    }
}