using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAssets.Data.Entity;

[Table(name: "Person", Schema = "ass")]
public class Person : BaseEntity
{
    [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }

    // [MaxLength(10)]
    // [MinLength(10)]
    [Length(10, 10, ErrorMessage = "مقدار کد ملی باید 10 رقم باشد")]
    [Required]
    public string NationalCode { get; set; }

    public List<PersonCar> PersonCars { get; set; } //many to many
}