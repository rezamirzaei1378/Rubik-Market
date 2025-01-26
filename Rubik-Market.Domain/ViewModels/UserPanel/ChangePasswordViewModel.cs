using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.UserPanel;

public class ChangePasswordViewModel
{
    public int UserId { get; set; }

    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
    public string RePassword { get; set; }
}

public enum ChangePasswordResult
{
    Success,
    CurrentPasswordIsIncorrect,
    UserNotFound,
    Error

}