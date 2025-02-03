using System.ComponentModel.DataAnnotations;

namespace PersonAssets.Data;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;
}