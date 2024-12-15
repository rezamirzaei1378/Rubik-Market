using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد کنید")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public enum ResultRegister
    {
        Success,
        Failed,
        EmailInvalid
    }
}
