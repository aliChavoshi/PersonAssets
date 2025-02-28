namespace PersonAssets.Models;

public class AuditableEntityViewModel
{
    public string CreatedBy { get; set; } //1 => Id (user name) (Email)
    public DateTime CreatedDateTime { get; set; } //2 => Id
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public int Version { get; set; }
    public bool IsActive { get; set; } 
}