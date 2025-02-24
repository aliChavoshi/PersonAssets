namespace PersonAssets.Data.Entity;

public class PersonCar
{
    public int CarId { get; set; }
    public int PersonId { get; set; }

    public Person Person { get; set; }
    public Car Car { get; set; }

}