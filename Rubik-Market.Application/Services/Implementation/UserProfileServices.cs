using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.User.Areas;
using Rubik_Market.Domain.ViewModels.UserPanel;

namespace Rubik_Market.Application.Services.Implementation
{
    public class UserProfileServices : IUserProfileServices
    {

        #region Constructor

        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;

        public UserProfileServices(IUserProfileRepository userProfileRepository, IUserRepository userRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region ForAdmin

        //Empty

        #endregion

        #region ForUserPanel

        public async Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return ChangePasswordResult.UserNotFound;
            }

            if (user.Password == model.CurrentPassword.EncodePasswordMd5())
            {
                return ChangePasswordResult.CurrentPasswordIsIncorrect;
            }
            try
            {
                user.Password = model.NewPassword.EncodePasswordMd5();
                _userRepository.UpdateUser(user);
                await _userRepository.SaveAsync();
                return ChangePasswordResult.Success;
            }
            catch
            {
                return ChangePasswordResult.Error;
            }
        }

        public async Task<UserPanelComponentViewModel?> IsProfileForAddOrEdit(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            bool profile = await IsUserHaveProfileAsync(userId);
            if (user == null)
            {
                return null;
            }

            UserPanelComponentViewModel model = new UserPanelComponentViewModel
            {
                FullName = user.FullName,
                ProfileIsForEdit = profile
            };
            
            return model;
        }

        #endregion

        #region ForSite

        //Empty

        #endregion

        #region Shared

        public async Task<UserProfileViewModel> GetUserProfileAsync(int? userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var profile = await _userProfileRepository.GetUserProfileAsync(userId);
            if (user == null)
            {
                return null!;
            }

            UserProfileViewModel model = new UserProfileViewModel()
            {
                UserId = user.ID,
                Email = user.Email,
                FullName = user.FullName,
                isActive = user.isActive,
                isAdmin = user.isAdmin,
                isDeleted = user.isDelete,
                UserCreateDate = user.CreateDate,

            };

            // ReSharper disable once RedundantBoolCompare
            if (profile == null || profile.isDelete == true)
            {
                model.CardNumberForRejectMoney = "-";
                model.NationalCode = "-";
                model.BirthDate = "-";
                model.CellPhoneNumber = "-";
                model.HousePhoneNumber = "-";
                model.AddOrEdit = "Add";
            }
            else
            {
                model.NationalCode = (string.IsNullOrWhiteSpace(profile.NationalCode) ? "-" : profile.NationalCode);
                model.BirthDate = (string.IsNullOrWhiteSpace(profile.BirthDate) ? "-" : profile.BirthDate.ToShamsiStr());
                model.CellPhoneNumber = (string.IsNullOrWhiteSpace(profile.CellPhoneNumber) ? "-" : profile.CellPhoneNumber);
                model.HousePhoneNumber = (string.IsNullOrWhiteSpace(profile.HousePhoneNumber) ? "-" : profile.HousePhoneNumber);
                model.CardNumberForRejectMoney = (string.IsNullOrWhiteSpace(profile.CardNumberForRejectMoney) ? "-" : profile.CardNumberForRejectMoney);
                model.AddOrEdit = "Edit";
            }

            return model;
        }

        public async Task<EditUserProfileViewModel> GetUserProfileForEditAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var profile = await _userProfileRepository.GetUserProfileAsync(userId);
            if (user == null)
            {
                return null!;
            }
            EditUserProfileViewModel model = new EditUserProfileViewModel()
            {
                Email = user.Email,
                FullName = user.FullName,
                UserId = userId,
                BirthDate = (string.IsNullOrWhiteSpace(profile!.BirthDate) ? "" : profile.BirthDate.ToShamsiStr()),
                CellPhoneNumber = profile.CellPhoneNumber,
                HousePhoneNumber = profile.HousePhoneNumber,
                CardNumberForRejectMoney = profile.CardNumberForRejectMoney,
                NationalCode = profile.NationalCode,
                GregorianBirthDate = profile.BirthDate,
                AddOrEdit = "Edit"
            };
            return model;
        }

        public async Task<AddUserProfileResult> AddUserProfileAsync(AddUserProfileViewModel model)
        {
            if (!await _userRepository.IsUserExistByIdAsync(model.UserId))
            {
                return AddUserProfileResult.UserNotFound;
            }
            List<string> listOfModelObject =
            [
                model.NationalCode!,
                model.CellPhoneNumber!,
                model.HousePhoneNumber!,
                model.BirthDate!,
                model.CardNumberForRejectMoney!
            ];

            if (listOfModelObject.All(string.IsNullOrEmpty))
            {
                return AddUserProfileResult.Error;
            }

            try
            {

                UserProfileInfo profile = new UserProfileInfo
                {
                    CreateDate = DateTime.Now,
                    isDelete = false,
                    UserId = model.UserId,
                    NationalCode = model.NationalCode,
                    CellPhoneNumber = model.CellPhoneNumber,
                    HousePhoneNumber = model.HousePhoneNumber,
                    BirthDate = (string.IsNullOrEmpty(model.BirthDate) ? null : model.BirthDate),
                    CardNumberForRejectMoney = model.CardNumberForRejectMoney,
                };

                await _userProfileRepository.AddUserProfileAsync(profile);
                await _userProfileRepository.SaveAsync();
                return AddUserProfileResult.Success;
            }
            catch
            {
                return AddUserProfileResult.Error;
            }
        }

        public async Task<EditUserProfileResult> EditUserProfileAsync(EditUserProfileViewModel model)
        {
            var userProfile = await _userProfileRepository.GetUserProfileAsync(model.UserId);
            if (userProfile == null)
            {
                return EditUserProfileResult.UserNotFound;
            }
            try
            {

                userProfile.UserId = model.UserId;
                userProfile.NationalCode = model.NationalCode;
                userProfile.CellPhoneNumber = model.CellPhoneNumber;
                userProfile.HousePhoneNumber = model.HousePhoneNumber;
                userProfile.CardNumberForRejectMoney = model.CardNumberForRejectMoney;
                userProfile.BirthDate = model.GregorianBirthDate;
                _userProfileRepository.UpdateUserProfileAsync(userProfile);
                await _userProfileRepository.SaveAsync();
                return EditUserProfileResult.Success;
            }
            catch
            {

                return EditUserProfileResult.Error;
            }
        }

        public async Task<EditUserProfileResult> AdminEditUserProfileAsync(AdminEditUserProfileViewModel model)
        {
            var profile = new EditUserProfileViewModel()
            {
                UserId = model.UserId,
                NationalCode = model.NationalCode,
                CardNumberForRejectMoney = model.CardNumberForRejectMoney,
                HousePhoneNumber = model.HousePhoneNumber,
                CellPhoneNumber = model.CellPhoneNumber,
            };
            var result = await EditUserProfileAsync(profile);

            return result;
        }

        public async Task<DeleteProfileResult> DeleteUserProfileAsync(int userId)
        {
            var userProfile = await _userProfileRepository.GetUserProfileAsync(userId);
            if (userProfile == null)
            {
                return DeleteProfileResult.UserNotFound;
            }

            try
            {
                userProfile.isDelete = true;
                _userProfileRepository.UpdateUserProfileAsync(userProfile);
                await _userProfileRepository.SaveAsync();

                return DeleteProfileResult.Success;
            }
            catch
            {
                return DeleteProfileResult.Error;
            }
        }

        public async Task<bool> IsUserHaveProfileAsync(int userId)
        {
            var profile = await _userProfileRepository.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return false;
            }
            else
            {
                List<string?> listOfModelObject =
                [
                    profile.NationalCode,
                    profile.CellPhoneNumber,
                    profile.HousePhoneNumber,
                    profile.BirthDate,
                    profile.CardNumberForRejectMoney
                ];
                if (listOfModelObject.All(l => l == null || l == " "))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

    }
}
