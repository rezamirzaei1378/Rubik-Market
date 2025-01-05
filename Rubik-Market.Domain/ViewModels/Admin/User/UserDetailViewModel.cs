using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.Admin.User;

public class UserDetailViewModel
{
    public int UserId { get; set; }
    public bool isDeleted { get; set; }
    public string Email { get; set; }
    public string? NationalCode { get; set; }
    public string? CellPhoneNumber { get; set; }
    public string? HousePhoneNumber { get; set; }
    public string? CardNumberForRejectMoney { get; set; }
    public string? BirthDate { get; set; }
    public DateTime UserCreateDate { get; set; }
}

public class UserCompoViewModel
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public bool isActive { get; set; }
    public bool isAdmin { get; set; }
}

public class AddUserDetailViewModel
{
    public int UserId { get; set; }

    [Display(Name = "کد ملی")]
    [MaxLength(10, ErrorMessage = "تعداد ارقام {0} باید 10 کارکتر باشد")]
    [MinLength(10, ErrorMessage = "تعداد ارقام {0} باید 10 کارکتر باشد")]
    public string? NationalCode { get; set; }
    [Display(Name = "شماره همراه")]
    [MaxLength(11, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    [MinLength(11, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    public string? CellPhoneNumber { get; set; }
    [Display(Name = "شماره ثابت")]
    [MaxLength(10, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    [MinLength(10, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    public string? HousePhoneNumber { get; set; }

    [Display(Name = "شماره کارت مرجوعی")]
    [MaxLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
    [MinLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
    public string? CardNumberForRejectMoney { get; set; }

}

public class EditUserDetailViewModel
{
    public int UserId { get; set; }

    [Display(Name = "کد ملی")]
    [MaxLength(10, ErrorMessage = "تعداد ارقام {0} باید 10 کارکتر باشد")]
    [MinLength(10, ErrorMessage = "تعداد ارقام {0} باید 10 کارکتر باشد")]
    public string? NationalCode { get; set; }
    [Display(Name = "شماره همراه")]
    [MaxLength(11, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    [MinLength(11, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    public string? CellPhoneNumber { get; set; }
    [Display(Name = "شماره ثابت")]
    [MaxLength(10, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    [MinLength(10, ErrorMessage = "تعداد ارقام {0} باید 11 کارکتر باشد")]
    public string? HousePhoneNumber { get; set; }

    [Display(Name = "شماره کارت مرجوعی")]
    [MaxLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
    [MinLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
    public string? CardNumberForRejectMoney { get; set; }

}