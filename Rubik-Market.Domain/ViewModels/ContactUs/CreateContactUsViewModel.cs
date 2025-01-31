using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.ContactUs;

public class CreateContactUsViewModel
{

    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(150, ErrorMessage = "تعداد کارکتر بیش از حد مجاز است")]
    public string FullName { get; set; }

    [Display(Name = "شماره تماس")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "تعداد کارکتر بیش از حد مجاز است")]
    public string? Mobile { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "تعداد کارکتر بیش از حد مجاز است")]
    [EmailAddress(ErrorMessage = "لطفا فرمت ایمیل معتبر را وارد کنید")]
    public string Email { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(350, ErrorMessage = "تعداد کارکتر بیش از حد مجاز است")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1000, ErrorMessage = "تعداد کارکتر بیش از حد مجاز است")]
    public string Description { get; set; }
}

public enum CreateContactUsResult
{
    Success,
    Error
}