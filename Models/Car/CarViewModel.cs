namespace PersonAssets.Models.Car;

public class CarViewModel : AuditableEntityViewModel
{
    public int Id { get; set; } //fake
    public string Color { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }
}