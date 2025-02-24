using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAssets.Data.Entity;

[Table(name: "Car", Schema = "ass")]
public class Car : AuditableEntity
{
    // [Required] public int PersonId { get; set; } //one to many
    public string Color { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }

    public List<PersonCar> PersonCars { get; set; } // many to many

    // #region MyRegion
    //
    // public Person Person { get; set; }

    // #endregion
}