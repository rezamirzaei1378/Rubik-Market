using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.User
{
    public class ActiveAccountViewModel
    {
        [Display(Name = "کد تایید ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ConfirmCode { get; set; }
    }

    public enum ResultActiveAccount
    {
        Success,
        Failed,
        UserNotFound
    }
}
