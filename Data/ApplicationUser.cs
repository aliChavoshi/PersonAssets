using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonAssets.Data;

public class ApplicationUser : IdentityUser
{
    [MaxLength(200, ErrorMessage = "تعداد کاراکتر ورودی باید 150 باشد")]
    [MinLength(3, ErrorMessage = "حداقل کاراکتر ورودی 3 میباشد")]
    // [Compare()]
    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [MaxLength(150, ErrorMessage = "تعداد کاراکتر ورودی باید 150 باشد")]
    [MinLength(3, ErrorMessage = "حداقل کاراکتر ورودی 3 میباشد")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }

    public bool IsActive { get; set; } = true;
}