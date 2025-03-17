using System.Security.AccessControl;

namespace PersonAssets.Models.Car;

public class AddOwnerViewModel
{
    public int CarId { get; set; }
    public string Color { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }

    public int OwnerId { get; set; } //Person
    public string OwnerName { get; set; }

    public List<AddOwnerViewModel> Owners { get; set; } = new();
}