using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.UserPanel;
using Rubik_Market.Web.Controllers;

namespace Rubik_Market.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserProfileController : Controller
    {
        #region Constructor

        private readonly IUserProfileServices _userProfileServices;

        public UserProfileController(IUserProfileServices userProfileServices)
        {
            _userProfileServices = userProfileServices;
        }

        #endregion

        #region UserProfile

        [Route("User-Profile")]
        public IActionResult Profile()
        {
            int userId = User.GetUserId();
            if (userId == null)
            {
                return NotFound();
                //TODO Error404
            }
            var model = _userProfileServices.GetUserPersonalInfo(userId);
            ViewData["Current-Page"] = "Profile";

            return View(model);
        }

        #endregion

        #region ChangePassword

        [HttpGet("Change-Password")]
        public IActionResult ChangePassword()
        {
            ViewData["Current-Page"] = "ChangePassword";
            return View();
        }

        [HttpPost("Change-Password")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            model.UserId = User.GetUserId();
            var result = _userProfileServices.ChangePassword(model);
            switch (result)
            {
                case ResultChangePassword.Success:
                    return RedirectToAction("Logout","Account");
                case ResultChangePassword.CurrentPasswordIsIncorrect:
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword),"کلمه عبور فعلی صحیح نیست");
                    break;
                case ResultChangePassword.UserNotFound:
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword), "کاربر یافت نشد");
                    break;
            }
            return View(model);
        }
        #endregion

    }
}
