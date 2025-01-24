using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAssets.Data;

[Table(name: "Person", Schema = "ass")]
public class Person : BaseEntity
{
    [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
    public string LastName { get; set; }

    // [MaxLength(10)]
    // [MinLength(10)]
    [Length(10, 10, ErrorMessage = "مقدار کد ملی باید 10 رقم باشد")]
    [Required]
    public string NationalCode { get; set; }

    #region Relations

    public List<Car> Cars { get; set; }

    #endregion
}