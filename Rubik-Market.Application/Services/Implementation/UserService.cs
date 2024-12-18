using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Generator;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
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


        public ResultRegister RegisterUser(RegisterViewModel model)
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

                _userRepository.AddUserAsync(user);
                _userRepository.SaveAsync();
                string emailBody = $"<h2>کد تایید شما {uniqueCode}</h2> ";
                _emailSender.SendEmail(model.UserEmail, "کد تایید حساب کاربری Rubik Market", emailBody);
                return ResultRegister.Success;
            }
            catch
            {
                return ResultRegister.Failed;
            }
        }

        public ResultActiveAccount ActiveAccount(ActiveAccountViewModel model)
        {

            var user = _userRepository.GetUserByConfirmCode(model.ConfirmCode);
            try
            {
                if (user == null || user.ConfirmCode != model.ConfirmCode)
                    return ResultActiveAccount.UserNotFound;

                user.isActive = true;
                user.ConfirmCode = null;
                _userRepository.UpdateUser(user);
                _userRepository.SaveAsync();
                return ResultActiveAccount.Success;
            }
            catch
            {
                return ResultActiveAccount.Failed;
            }



        }

        public ResultLogin LoginUser(LoginViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.UserEmail.Trim().ToLower());

            if (user == null)
                return ResultLogin.UserNotFount;
            if (user.Password != model.Password.EncodePasswordMd5())
                return ResultLogin.UserNotFount;

            return ResultLogin.Success;
        }

        public User GetUserByEmail(string email)
        {
           return _userRepository.GetUserByEmail(email);
        }

        public ResultForgetPassword ForgetPassword(ForgetPasswordViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.UserEmail.Trim().ToLower());
            if (user == null)
                return ResultForgetPassword.UserNotFound;
            string uniqueCode = CodeGenerator.UniqueCode();

            user.ConfirmCode = uniqueCode;
            _userRepository.UpdateUser(user);
            _userRepository.SaveAsync();

            string emailBody = $"<h2>کد تایید شما {uniqueCode}</h2> ";
            _emailSender.SendEmail(model.UserEmail, "کد تایید بازیابی کلمه عبور Rubik Market", emailBody);

            return ResultForgetPassword.Success;
        }

        public ResultResetPassword ResetPassword(ResetPasswordViewModel model)
        {
            var user = _userRepository.GetUserByConfirmCode(model.ConfirmCode);

            if (user == null)
            {
                return ResultResetPassword.UserNotFound;
            }

            user.Password=model.Password.EncodePasswordMd5();
            user.ConfirmCode = null;
            _userRepository.UpdateUser(user);
            _userRepository.SaveAsync();
            return ResultResetPassword.Success;
        }
    }
}
