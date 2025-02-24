namespace PersonAssets.Data.Entity;

public class AuditableEntity : BaseEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public int Version { get; set; } = 0; //modify => version++
    public bool IsActive { get; set; } = true;

    public ApplicationUser CreateUser { get; set; }
    public ApplicationUser ModifyUser { get; set; }
}