using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.User
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string UserEmail { get; set; }
    }

    public enum ResultForgetPassword
    {
        Success,
        UserNotFound
    }
}
