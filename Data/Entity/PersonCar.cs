namespace PersonAssets.Data.Entity;

public class PersonCar
{
    public int CarId { get; set; }
    public int PersonId { get; set; }
    public bool IsConfirmed { get; set; } = false;

    public Person Person { get; set; } //entity
    public Car Car { get; set; } //entity
}