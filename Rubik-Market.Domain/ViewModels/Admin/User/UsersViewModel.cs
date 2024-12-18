using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.Admin.User
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربر را وارد کنید")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isAdmin { get; set; } = false;
        public bool isActive { get; set; } = false;
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام کاربر را وارد کنید")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string UserEmail { get; set; }
        public bool isAdmin { get; set; } = false;
        public bool isActive { get; set; } = false;
    }
}
