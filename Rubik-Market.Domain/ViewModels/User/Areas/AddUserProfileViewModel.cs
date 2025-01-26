using System.ComponentModel.DataAnnotations;


namespace Rubik_Market.Domain.ViewModels.User.Areas
{
    public class AddUserProfileViewModel
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
        public string? BirthDate { get; set; }

    }

    public enum AddUserProfileResult
    {
        Success,
        UserNotFound,
        Error
    }
}
