using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.UserPanel;

namespace Rubik_Market.Application.Services.Implementation
{
    public class UserProfileServices(IUserRepository userRepository, IUserPersonalInfoRepository userPersonalInfoRepository) : IUserProfileServices
    {

        public UserPersonalInfoViewModel GetUserPersonalInfo(int id)
        {
            var user = userRepository.GetUserById(id);
            var userPersonalInfo = userPersonalInfoRepository.GetUserPersonalInfo(id);
            if (user == null || userPersonalInfo == null)
            {
                return null;
            }
            else
            {
                UserPersonalInfoViewModel model = new UserPersonalInfoViewModel();
                model.Email = user.Email;
                model.FullName = user.FullName;
                model.isActive = user.isActive;
                model.isAdmin = user.isAdmin;
                model.UserCreateDate = user.CreateDate;
                if (userPersonalInfo == null)
                {
                    model.CardNumberForRejectMoney = "-";
                    model.NationalCode = "-";
                    model.BirthDate = "-";
                    model.CellPhoneNumber = "-";
                    model.HousePhoneNumber = "-";
                }
                else
                {
                    model.NationalCode = userPersonalInfo.NationalCode ?? "-";
                    model.HousePhoneNumber = userPersonalInfo.HousePhoneNumber ?? "-";
                    model.BirthDate = (userPersonalInfo.BirthDate == null) ? "-" : userPersonalInfo.BirthDate.ToShamsiStr();
                    model.CardNumberForRejectMoney = (userPersonalInfo.CardNumberForRejectMoney == null) ? "-" :
                      userPersonalInfo.CardNumberForRejectMoney.ChangeCardShowFormat();
                    model.CellPhoneNumber = userPersonalInfo.CellPhoneNumber ?? "-";
                }

                return model;
            }
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

        public AddUserPersonalInfoResult AddUserPersonalInfo(AddUserPersonalInfoViewModel model)
        {
            try
            {
                var user = userRepository.GetUserById(model.UserId);

                if (user == null)
                    return AddUserPersonalInfoResult.UserNotFound;

                UserProfileInfo profile = new UserProfileInfo()
                {
                    UserId = model.UserId,
                    BirthDate = model.BirthDate,
                    CardNumberForRejectMoney = model.CardNumberForRejectMoney,
                    CellPhoneNumber = model.CellPhoneNumber,
                    HousePhoneNumber = model.HousePhoneNumber,
                    NationalCode = model.NationalCode
                };

                userPersonalInfoRepository.AddUserPersonalInfo(profile);
                userPersonalInfoRepository.SaveAsync();
                return AddUserPersonalInfoResult.Success;
            }
            catch
            {
                return AddUserPersonalInfoResult.Failed;
            }
        }

        public bool IsUserProfileFill(int id)
        {
            return userPersonalInfoRepository.IsUserProfileExist(id);
        }

        public EditUserPersonalInfoViewModel GetUserPersonalInfoToEdit(int id)
        {
            var user = userRepository.GetUserById(id);
            var userPersonalInfo = userPersonalInfoRepository.GetUserPersonalInfo(id);
            EditUserPersonalInfoViewModel model = new EditUserPersonalInfoViewModel()
            {
                FullName = user.FullName,
                Email = user.Email,
                NationalCode = userPersonalInfo.NationalCode ?? "",
                HousePhoneNumber = userPersonalInfo.HousePhoneNumber ?? "",
                BirthDate = (userPersonalInfo.BirthDate == null) ? "" : userPersonalInfo.BirthDate.ToShamsiStr(),
                GregorianBirthDate = userPersonalInfo.BirthDate,
                CardNumberForRejectMoney = (userPersonalInfo.CardNumberForRejectMoney == null) ? "" :
                  userPersonalInfo.CardNumberForRejectMoney,
                CellPhoneNumber = userPersonalInfo.CellPhoneNumber ?? "-"
            };
            return model;
        }

        public async Task<EditUserPersonalInfoResult> EditUserPersonalInfo(EditUserPersonalInfoViewModel model)
        {
            try
            {
                var userExist = userRepository.GetUserById(model.UserId);
                var userProfile = userPersonalInfoRepository.GetUserPersonalInfo(model.UserId);

                if (userExist == null)
                    return EditUserPersonalInfoResult.UserNotFound;
                User user = new User()
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    Password = userExist.Password,
                    ConfirmCode = null,
                    CreateDate = userExist.CreateDate,
                    ID = userExist.ID,
                    isActive = userExist.isActive,
                    isDelete = userExist.isDelete,
                    isAdmin = userExist.isAdmin
                };

                userRepository.UpdateUser(user);
                await userRepository.SaveAsync();

                UserProfileInfo profile = new UserProfileInfo()
                {
                    UserId = model.UserId,
                    BirthDate = model.GregorianBirthDate,
                    CardNumberForRejectMoney = model.CardNumberForRejectMoney,
                    CellPhoneNumber = model.CellPhoneNumber,
                    HousePhoneNumber = model.HousePhoneNumber,
                    NationalCode = model.NationalCode,
                    CreateDate = userProfile.CreateDate,
                    ID = userProfile.ID,
                    isDelete = userProfile.isDelete
                };
                userPersonalInfoRepository.UpdateUserPersonalInfo(profile);
                await userPersonalInfoRepository.SaveAsync();
                return EditUserPersonalInfoResult.Success;
            }
            catch
            {
                return EditUserPersonalInfoResult.Failed;
            }
        }
    }
}
