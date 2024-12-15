using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public enum ResultLogin
    {
        Success,
        UserNotFount
    }
}
