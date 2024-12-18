using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.UserPanel;

namespace Rubik_Market.Application.Services.Implementation
{
    public class UserProfileServices(IUserRepository userRepository):IUserProfileServices
    {

        public UserPersonalInfoViewModel GetUserPersonalInfo(int id)
        {
            var user = userRepository.GetUserById(id);

            UserPersonalInfoViewModel model = new UserPersonalInfoViewModel()
            {
                Email = user.Email,
                FullName = user.FullName,
                isActive = user.isActive,
                isAdmin = user.isAdmin,
                CreateDate = user.CreateDate
            };

            return model;
        }

        public ResultChangePassword ChangePassword(ChangePasswordViewModel model)
        {
            if (!model.UserId.HasValue)
            {
                return ResultChangePassword.UserNotFound;
            }

            var user = userRepository.GetUserById(model.UserId.Value);
            if (user == null) 
                return ResultChangePassword.UserNotFound;

            if (user.Password != model.CurrentPassword.EncodePasswordMd5())
                return ResultChangePassword.CurrentPasswordIsIncorrect;

            user.Password = model.NewPassword.EncodePasswordMd5();
            userRepository.UpdateUser(user);
            userRepository.SaveAsync();

            return ResultChangePassword.Success;
        }
    }
}
