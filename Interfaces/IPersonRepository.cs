using PersonAssets.Data;
using PersonAssets.Data.Entity;

namespace PersonAssets.Interfaces;

public interface IPersonRepository
{
    Task Create(Person person); // Task == await , async
    Task<Person> GetPersonById(int id);
    Task<List<Person>> GetAllPersons(string search);
    Task<List<Person>> GetDeletedUsers(); //Rule
    Task<Person> Edit(Person person);
    Task Delete(int id);
    Task Delete(Person person);
    Task Save();
    Task<bool> IsAnyNationalCode(string nationalCode);
}