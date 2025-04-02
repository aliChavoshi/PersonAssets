using System.ComponentModel.DataAnnotations;

namespace PersonAssets.Models.Car;

public class ConfirmPersonCarViewModel
{
    public int CarId { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }

    //
    public int PersonId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string NationalCode { get; set; }

    //
    public bool IsConfirmed { get; set; }
}