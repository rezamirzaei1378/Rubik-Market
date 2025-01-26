using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.User.Areas
{
    public class EditUserProfileViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }

        [Display(Name = "کارت ملی")]
        [MaxLength(10, ErrorMessage = "تعداد ارقام {0} 10 کارکتر باشد")]
        [MinLength(10, ErrorMessage = "تعداد ارقام {0} 10 کارکتر باشد")]
        public string? NationalCode { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(11, ErrorMessage = "تعداد  ارقام{0} باید 11 کارکتر باشد")]
        [MinLength(11, ErrorMessage = "تعداد  ارقام{0} باید 11 کارکتر باشد")]
        public string? CellPhoneNumber { get; set; }

        [Display(Name = "شماره ثابت")]
        [MaxLength(11, ErrorMessage = "تعداد ارقام {0} 11 کارکتر باشد")]
        [MinLength(11, ErrorMessage = "تعداد ارقام {0} 11 کارکتر باشد")]
        public string? HousePhoneNumber { get; set; }
        public string? BirthDate { get; set; }
        public string? GregorianBirthDate { get; set; }

        [Display(Name = "شماره کارت جهت مرجوعی")]
        [MaxLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
        [MinLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
        public string? CardNumberForRejectMoney { get; set; }
        public string? AddOrEdit { get; set; }
    }
    public class AdminEditUserProfileViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "کارت ملی")]
        [MaxLength(10, ErrorMessage = "تعداد ارقام {0} 10 کارکتر باشد")]
        [MinLength(10, ErrorMessage = "تعداد ارقام {0} 10 کارکتر باشد")]
        public string? NationalCode { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(11, ErrorMessage = "تعداد  ارقام{0} باید 11 کارکتر باشد")]
        [MinLength(11, ErrorMessage = "تعداد  ارقام{0} باید 11 کارکتر باشد")]
        public string? CellPhoneNumber { get; set; }

        [Display(Name = "شماره ثابت")]
        [MaxLength(11, ErrorMessage = "تعداد ارقام {0} 11 کارکتر باشد")]
        [MinLength(11, ErrorMessage = "تعداد ارقام {0} 11 کارکتر باشد")]
        public string? HousePhoneNumber { get; set; }

        [Display(Name = "شماره کارت جهت مرجوعی")]
        [MaxLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
        [MinLength(16, ErrorMessage = "تعداد ارقام {0} باید 16 کارکتر باشد")]
        public string? CardNumberForRejectMoney { get; set; }
        public string? AddOrEdit { get; set; }
    }

    public enum EditUserProfileResult
    {
        Success,
        UserNotFound,
        Error
    }
}
