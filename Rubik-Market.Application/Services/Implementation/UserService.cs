using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Generator;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Admin.User;
using Rubik_Market.Domain.ViewModels.User;

namespace Rubik_Market.Application.Services.Implementation
{
    public class UserService : IUserServices
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public UserService(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        #endregion


        
        #region ForAdmin

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<List<User>> GetAllDeletedUsersAsync()
        {
            return await _userRepository.GetAllDeletedUsersAsync();
        }

        public async Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model)
        {
            try
            {
                if (_userRepository.IsUserExistByEmailAsync(model.UserEmail.Trim().ToLower()))
                {
                    return CreateUserResult.UserExist;
                }

                User user = new User()
                {
                    FullName = model.FullName,
                    Email = model.UserEmail,
                    CreateDate = DateTime.Now,
                    ConfirmCode = null,
                    Password = model.Password.EncodePasswordMd5(),
                    isAdmin = model.isAdmin,
                    isActive = model.isActive,
                    isDelete = false
                };
                await _userRepository.AddUserAsync(user);
                await _userRepository.SaveAsync();
                return CreateUserResult.Success;
            }
            catch
            {
                return CreateUserResult.Error;
            }
        }

        public async Task<EditUserResult> UpdateUserAsync(EditUserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(model.Id);
                
                if (user == null)
                    return EditUserResult.UserNotExist;

                user.FullName = model.FullName;
                user.Email = model.UserEmail;
                user.isActive = model.isActive;
                user.isAdmin = model.isAdmin;

                _userRepository.UpdateUser(user);
                await _userRepository.SaveAsync();
                return EditUserResult.Success;
            }
            catch
            {
                return EditUserResult.Error;
            }

        }

        public async Task<DeleteUserResult> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return DeleteUserResult.UserNotExist;
            try
            {
                user.isDelete = true;
                _userRepository.DeleteUser(user);
                await _userRepository.SaveAsync();
                return DeleteUserResult.Success;
            }
            catch
            {
                return DeleteUserResult.Error;
            }

        }

        public async Task<User?> GetUserByIdAsync(int? id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> IsUserExistByIdAsync(int? id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<EditUserViewModel?> GetUserByIdForEditAsync(int? id)
        {
            var user = await GetUserByIdAsync(id);
            return new EditUserViewModel()
            {
                FullName = user!.FullName,
                Id = user.ID,
                isActive = user.isActive,
                isAdmin = user.isAdmin,
                UserEmail = user.Email
            };
        }

        public bool IsUserExistByEmail(string email)
        {
            return _userRepository.IsUserExistByEmailAsync(email);
        }

        #endregion

        #region ForUserPanel

        public async Task<ResultResetPassword> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userRepository.GetUserByConfirmCodeAsync(model.ConfirmCode);

            if (user == null)
            {
                return ResultResetPassword.UserNotFound;
            }

            user.Password = model.Password.EncodePasswordMd5();
            user.ConfirmCode = null;
            _userRepository.UpdateUser(user);
            await _userRepository.SaveAsync();
            return ResultResetPassword.Success;
        }

        #endregion

        #region ForSite

        public async Task<ResultRegister> RegisterUserAsync(RegisterViewModel model)
        {
            try
            {
                if (_userRepository.IsUserExistByEmailAsync(model.UserEmail.Trim().ToLower()))
                {
                    return ResultRegister.EmailInvalid;
                }

                string uniqueCode = CodeGenerator.UniqueCode();
                User user = new User()
                {
                    FullName = model.FullName,
                    Password = model.Password.EncodePasswordMd5(),
                    Email = model.UserEmail,
                    ConfirmCode = uniqueCode
                };

                await _userRepository.AddUserAsync(user);
                await _userRepository.SaveAsync();
                string emailBody = $"<h2>کد تایید شما {uniqueCode}</h2> ";
                _emailSender.SendEmail(model.UserEmail, "کد تایید حساب کاربری Rubik Market", emailBody);
                return ResultRegister.Success;
            }
            catch
            {
                return ResultRegister.Failed;
            }
        }
        public async Task<ResultActiveAccount> ActiveAccountAsync(ActiveAccountViewModel model)
        {

            var user = await _userRepository.GetUserByConfirmCodeAsync(model.ConfirmCode);
            try
            {
                if (user == null || user.ConfirmCode != model.ConfirmCode)
                    return ResultActiveAccount.UserNotFound;

                user.isActive = true;
                user.ConfirmCode = null;
                _userRepository.UpdateUser(user);
                await _userRepository.SaveAsync();
                return ResultActiveAccount.Success;
            }
            catch
            {
                return ResultActiveAccount.Failed;
            }
        }
        public async Task<ResultLogin> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.UserEmail.Trim().ToLower());

            if (user == null)
                return ResultLogin.UserNotFount;
            if (user.Password != model.Password.EncodePasswordMd5())
                return ResultLogin.UserNotFount;

            return ResultLogin.Success;
        }
        public async Task<ResultForgetPassword> ForgetPasswordAsync(ForgetPasswordViewModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.UserEmail.Trim().ToLower());
            if (user == null)
                return ResultForgetPassword.UserNotFound;
            string uniqueCode = CodeGenerator.UniqueCode();

            user.ConfirmCode = uniqueCode;
            _userRepository.UpdateUser(user);
            await _userRepository.SaveAsync();

            string emailBody = $"<h2>کد تایید شما {uniqueCode}</h2> ";
            _emailSender.SendEmail(model.UserEmail, "کد تایید بازیابی کلمه عبور Rubik Market", emailBody);

            return ResultForgetPassword.Success;
        }
        #endregion

        #region Shared

        public async Task<User?> GetUserByEmailAsync(string? email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        #endregion
    }
}
