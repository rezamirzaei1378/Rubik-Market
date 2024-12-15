using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.ViewModels.User;

namespace Rubik_Market.Application.Services.Contracts
{
    public interface IUserServices
    {
        ResultRegister RegisterUser(RegisterViewModel model);
        ResultActiveAccount ActiveAccount(ActiveAccountViewModel model);
        ResultLogin LoginUser(LoginViewModel model);
        User GetUserByEmail(string email);
        ResultForgetPassword ForgetPassword (ForgetPasswordViewModel  model);
        ResultResetPassword ResetPassword(ResetPasswordViewModel  model);
    }
}
