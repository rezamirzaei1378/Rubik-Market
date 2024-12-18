using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.ViewModels.UserPanel;

namespace Rubik_Market.Application.Services.Contracts
{
    public interface IUserProfileServices
    {
        public UserPersonalInfoViewModel GetUserPersonalInfo(int id);

        ResultChangePassword ChangePassword(ChangePasswordViewModel model);
    }
}
