using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PersonAssets.Models.Person;

public class EditPersonViewModel
{
    [HiddenInput(DisplayValue = true)]
    public int Id { get; set; }

    [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }

    [Length(10, 10, ErrorMessage = "مقدار کد ملی باید 10 رقم باشد")]
    [Required]
    public string NationalCode { get; set; }
}