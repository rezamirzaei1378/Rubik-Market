using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.UserPanel;

public class EditUserPersonalInfoViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد کنید")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
    public string Email { get; set; }

    [MaxLength(10,ErrorMessage = "تعداد ارقام کارت ملی باید 10 کارکتر باشد")]
    [MinLength(10,ErrorMessage = "تعداد ارقام کارت ملی باید 10 کارکتر باشد")]
    public string? NationalCode { get; set; }
    [MaxLength(11, ErrorMessage = "تعداد ارقام شماره همراه باید 11 کارکتر باشد")]
    [MinLength(11, ErrorMessage = "تعداد ارقام شماره همراه باید 11 کارکتر باشد")]
    public string? CellPhoneNumber { get; set; }
    [MaxLength(11, ErrorMessage = "تعداد ارقام شماره ثابت باید 11 کارکتر باشد")]
    [MinLength(11, ErrorMessage = "تعداد ارقام شماره ثابت باید 11 کارکتر باشد")]
    public string? HousePhoneNumber { get; set; }
    public string? GregorianBirthDate { get; set; }
    public string? BirthDate { get; set; }

    [MaxLength(16, ErrorMessage = "تعداد ارقام شماره کارت جهت مرجوعی باید 16 کارکتر باشد")]
    [MinLength(16, ErrorMessage = "تعداد ارقام شماره کارت جهت مرجوعی باید 16 کارکتر باشد")]
    public string? CardNumberForRejectMoney { get; set; }
}

public enum EditUserPersonalInfoResult
{
    Success,
    UserNotFound,
    Failed
}