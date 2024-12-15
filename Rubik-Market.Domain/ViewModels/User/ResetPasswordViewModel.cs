using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا کد تایید ارسال شده به ایمیل را وارد کنید")]
        public string ConfirmCode { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public enum ResultResetPassword
    {
        Success,
        UserNotFound
    }
}
