using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace PersonAssets.Data;

[Table(name: "Car", Schema = "ass")]
public class Car : BaseEntity
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