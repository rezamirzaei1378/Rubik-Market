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
            var model = _userProfileServices.GetUserPersonalInfo(userId);
            if (model == null)
                return View("ErrorPages/Error404");

            if (_userProfileServices.IsUserProfileFill(userId))
            {
                ViewData["AddOrEdit"] = "Edit";
            }
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

        #region AddUserPersonalInfo

        [HttpGet("AddUserPersonalInfo")]
        public IActionResult AddUserPersonalInfo()
        {
            @ViewData["Current-Page"] = "UserPersonalInfo";
            return View();
        }

        [HttpPost("AddUserPersonalInfo")]
        [ValidateAntiForgeryToken]
        public IActionResult AddUserPersonalInfo(AddUserPersonalInfoViewModel model)
        {

            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            int userId = User.GetUserId();
            model.UserId =userId;

            var result = _userProfileServices.AddUserPersonalInfo(model);
            switch (result)
            {
                case AddUserPersonalInfoResult.Success:
                    TempData["Message"] = "ProfileAddSuccess";
                    return RedirectToAction(nameof(Profile));
                case AddUserPersonalInfoResult.UserNotFound:
                    ModelState.AddModelError(nameof(AddUserPersonalInfoViewModel.NationalCode),"کاربر یاقت نشد");
                    return View(model);
                case AddUserPersonalInfoResult.Failed:
                    TempData["Massage"] = "FailedProfile";
                    return View(model);
            }
            
            _userProfileServices.AddUserPersonalInfo(model);
            ViewData["Current-Page"] = "UserPersonalInfo";
            return View();
        }

        #endregion

        #region EditUserPersonalInfo

        [HttpGet("EditUserPersonalInfo")]
        
        public IActionResult EditUserPersonalInfo(int userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var userProfile = _userProfileServices.GetUserPersonalInfoToEdit(userId);
            if (userProfile == null)
            {
                return NotFound();
            }
            ViewData["AddOrEdit"] = "Edit";
            ViewData["Current-Page"] = "UserPersonalInfo";
            return View(userProfile);
        }

        [HttpPost("EditUserPersonalInfo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserPersonalInfo(EditUserPersonalInfoViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion
            int userId = User.GetUserId();
            model.UserId = userId;

            var result = await _userProfileServices.EditUserPersonalInfo(model);
            switch (result)
            {
                case EditUserPersonalInfoResult.Success:
                    TempData["Message"] = "ProfileEditSuccess";
                    return RedirectToAction(nameof(Profile));
                case EditUserPersonalInfoResult.UserNotFound:
                    ModelState.AddModelError(nameof(AddUserPersonalInfoViewModel.NationalCode), "کاربر یاقت نشد");
                    return View(model);
                case EditUserPersonalInfoResult.Failed:
                    TempData["Massage"] = "FailedEditProfile";
                    return View(model);
            }

            ViewData["Current-Page"] = "UserPersonalInfo";
            return View();
        }
        #endregion

    }
}
