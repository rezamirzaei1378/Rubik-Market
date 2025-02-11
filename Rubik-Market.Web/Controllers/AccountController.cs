﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.User;

namespace Rubik_Market.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Constuctor

        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #endregion

        #region Register
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await _userServices.RegisterUserAsync(model);
            switch (result)
            {
                case ResultRegister.Success:
                    {
                        //ModelState.AddModelError("UserEmail", "ثبت نام موفق بود");
                        TempData["Massage"] = "RegisterSuccess";
                        return RedirectToAction(nameof(ActiveAccount));
                    }
                case ResultRegister.Failed:
                    TempData["Massage"] = "Failed";
                    return View(model);
                case ResultRegister.EmailInvalid:
                    {
                        ModelState.AddModelError("UserEmail", "ایمیل وارد شده موجود است");
                        return View(model);
                    }
            }
            return View(model);
        }

        #endregion

        #region ActiveAccount

        [HttpGet("ActiveAccount")]
        public IActionResult ActiveAccount()
        {
            return View();
        }
        [HttpPost("ActiveAccount")]
        public async Task<IActionResult> ActiveAccount(ActiveAccountViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userServices.ActiveAccountAsync(model);

            switch (result)
            {
                case ResultActiveAccount.Success:
                    TempData["Massage"] = "ActiveSuccess";
                    return RedirectToAction(nameof(Login));
                case ResultActiveAccount.Failed:
                    ModelState.AddModelError(nameof(ActiveAccountViewModel.ConfirmCode), "خطای  سرور ");
                    break;
                case ResultActiveAccount.UserNotFound:
                    ModelState.AddModelError(nameof(ActiveAccountViewModel.ConfirmCode), "کد وارد شده صحیح نمی باشد");
                    break;
            }

            return View();
        }

        #endregion

        #region Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await _userServices.LoginUserAsync(model);

            switch (result)
            {
                case ResultLogin.Success:
                    var user = await _userServices.GetUserByEmailAsync(model.UserEmail);
                    if (user == null)
                    {
                        ModelState.AddModelError(nameof(LoginViewModel.UserEmail), "اطلاعات وارد شده صحیح نمی باشد");
                        return View(model);
                    }

                    if (user.isActive == false)
                    {
                        ModelState.AddModelError(nameof(LoginViewModel.UserEmail), " کاربر فعال نمی باشد");
                        return View(model);
                    }

                    #region Claim

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserFullName", user.FullName)
                    };

                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                    var authenticationProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true,
                    };
                    #endregion

                    TempData["Massage"] = "LoginSuccess";

                    #region Login

                    await HttpContext.SignInAsync(claimPrincipal, authenticationProperties);
                    return Redirect("/");

                    #endregion
                case ResultLogin.UserNotFount:
                    ModelState.AddModelError(nameof(LoginViewModel.UserEmail), "اطلاعات وارد شده صحیح نمی باشد");
                    break;
            }

            return View(model);
        }

        #endregion

        #region Logout

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region ForgetPassword

        [HttpGet("Forgot-Password")]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userServices.ForgetPasswordAsync(model);

            switch (result)
            {
                case ResultForgetPassword.Success:
                    return RedirectToAction(nameof(ResetPassword));
                case ResultForgetPassword.UserNotFound:
                    ModelState.AddModelError(nameof(ForgetPasswordViewModel.UserEmail), "طلاعات وارد شده صحیح نمی باشد");
                    break;
            }
            return View();
        }

        #endregion

        #region ResetPassword

        [HttpGet("Reset-Password")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userServices.ResetPasswordAsync(model);

            switch (result)
            {
                case ResultResetPassword.Success:
                    return RedirectToAction(nameof(Login));
                case ResultResetPassword.UserNotFound:
                    return RedirectToAction(nameof(ForgetPassword));
            }
            return View(model);
        }

        #endregion
    }
}
