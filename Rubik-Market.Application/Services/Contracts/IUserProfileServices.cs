using Rubik_Market.Domain.ViewModels.User.Areas;
using Rubik_Market.Domain.ViewModels.UserPanel;

namespace Rubik_Market.Application.Services.Contracts
{
    public interface IUserProfileServices
    {
        //ResultChangePassword ChangePassword(ChangePasswordViewModel model);
        //public bool IsUserProfileFill (int id);

        #region ForAdmin

        //Empty

        #endregion

        #region ForUserPanel

        Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel model);
        Task<UserPanelComponentViewModel?> IsProfileForAddOrEdit(int userId);

        #endregion

        #region ForSite

        //Empty

        #endregion

        #region Shared

        Task<UserProfileViewModel> GetUserProfileAsync(int? userId);
        Task<EditUserProfileViewModel> GetUserProfileForEditAsync(int userId);
        Task<AddUserProfileResult> AddUserProfileAsync(AddUserProfileViewModel model);
        Task<EditUserProfileResult> EditUserProfileAsync(EditUserProfileViewModel model);
        Task<EditUserProfileResult> AdminEditUserProfileAsync(AdminEditUserProfileViewModel model);
        Task<DeleteProfileResult> DeleteUserProfileAsync(int userId);
        Task<bool> IsUserHaveProfileAsync(int userId);

        #endregion
    }
}
