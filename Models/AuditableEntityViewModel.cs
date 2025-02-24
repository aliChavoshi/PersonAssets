namespace PersonAssets.Models;

public class AuditableEntityViewModel
{
    public string CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public int Version { get; set; }
    public bool IsActive { get; set; } 
}