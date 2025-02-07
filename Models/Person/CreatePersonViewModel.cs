using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PersonAssets.Models.Person;

public class CreatePersonViewModel
{
    [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
    [Display(Name = "نام")]
    [MinLength(3, ErrorMessage = "حداقل اطلاعات وارد شده 3 کاراکتر میباشد")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
    [Display(Name = "نام خانوادگی")]
    [MaxLength(15,ErrorMessage = "تعداد حرف های وارد شده برای نام خانوادگی غیر مجاز میباشد")]
    public string LastName { get; set; }

    [Length(10, 10, ErrorMessage = "مقدار کد ملی باید 10 رقم باشد")]
    [Required]
    // [Remote()]
    public string NationalCode { get; set; }
}