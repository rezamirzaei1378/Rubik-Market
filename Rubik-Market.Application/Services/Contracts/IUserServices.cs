using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.ViewModels.Admin.User;
using Rubik_Market.Domain.ViewModels.User;

namespace Rubik_Market.Application.Services.Contracts
{
    public interface IUserServices
    {
        #region ForAdmin
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllDeletedUsersAsync();
        Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model);
        Task<EditUserResult> UpdateUserAsync(EditUserViewModel model);
        Task<DeleteUserResult> DeleteUserAsync(int id);
        Task<User?> GetUserByIdAsync(int? id);
        Task<bool> IsUserExistByIdAsync(int? id);
        Task<EditUserViewModel?> GetUserByIdForEditAsync(int? id);
        public bool IsUserExistByEmail(string email);
        #endregion

        #region ForUserPanel

        Task<ResultResetPassword> ResetPasswordAsync(ResetPasswordViewModel model);

        #endregion

        #region ForSite
        Task<ResultRegister> RegisterUserAsync(RegisterViewModel model);
        Task<ResultActiveAccount> ActiveAccountAsync(ActiveAccountViewModel model);
        Task<ResultLogin> LoginUserAsync(LoginViewModel model);

        Task<ResultForgetPassword> ForgetPasswordAsync(ForgetPasswordViewModel model);
        #endregion

        #region Shared

        Task<User?> GetUserByEmailAsync(string? email);

        #endregion
    }

  

}
