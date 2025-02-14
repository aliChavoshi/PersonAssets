using PersonAssets.Data;

namespace PersonAssets.Interfaces;

public interface IPersonRepository
{
    Task Create(Person person); // Task == await , async
    Task<List<Person>> GetAllPersons();
    Task<Person> Edit(Person person);
    Task Delete(int id);
    Task Delete(Person person);
    Task Save();
    Task<bool> IsAnyNationalCode(string nationalCode);
}