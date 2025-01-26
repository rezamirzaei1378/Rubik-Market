using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.User.Areas;
using Rubik_Market.Domain.ViewModels.UserPanel;

namespace Rubik_Market.Web.Areas.UserPanel.Controllers
{

    public class UserProfileController : UserPanelBaseController
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
        public async Task<IActionResult> Profile()
        {
            int userId = User.GetUserId();
            var model = await _userProfileServices.GetUserProfileAsync(userId);

            if (model == null)
                return View("ErrorPages/Error404");

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
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            model.UserId = User.GetUserId();
            var result = await _userProfileServices.ChangePassword(model);
            switch (result)
            {
                case ChangePasswordResult.Success:
                    return RedirectToAction("Logout","Account");
                case ChangePasswordResult.CurrentPasswordIsIncorrect:
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword),"کلمه عبور فعلی صحیح نیست");
                    break;
                case ChangePasswordResult.UserNotFound:
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword), "کاربر یافت نشد");
                    break;
                case ChangePasswordResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return View(model);
        }
        #endregion

        #region AddUserPersonalInfo

        [HttpGet("AddUserPersonalInfo")]
        public async Task<IActionResult> AddUserPersonalInfo()
        {
            var id = User.GetUserId();
            var userProfile = await _userProfileServices.GetUserProfileAsync(id);
            if (userProfile.AddOrEdit == "Edit")
            {
                return RedirectToAction("EditUserPersonalInfo",new { userId =  id});
            }
            ViewData["Current-Page"] = "UserPersonalInfo";
            return View();
        }

        [HttpPost("AddUserPersonalInfo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserPersonalInfo(AddUserProfileViewModel model)
        {

            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            int userId = User.GetUserId();
            model.UserId = userId;

            var result = await _userProfileServices.AddUserProfileAsync(model);
            switch (result)
            {
                case AddUserProfileResult.Success:
                    TempData["Message"] = "ProfileAddSuccess";
                    return RedirectToAction(nameof(Profile));
                case AddUserProfileResult.UserNotFound:
                    ModelState.AddModelError(nameof(AddUserProfileViewModel.NationalCode),"کاربر یاقت نشد");
                    return View(model);
                case AddUserProfileResult.Error:
                    TempData["Massage"] = "FailedProfile";
                    return View();
            }
            
            ViewData["Current-Page"] = "UserPersonalInfo";
            return View();
        }

        #endregion

        #region EditUserPersonalInfo

        [HttpGet("EditUserPersonalInfo")]
        
        public async Task<IActionResult> EditUserPersonalInfo(int userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var userProfile = await _userProfileServices.GetUserProfileForEditAsync(userId);
            if (userProfile == null)
            {
                return NotFound();
            }
            ViewData["Current-Page"] = "UserPersonalInfo";
            return View(userProfile);
        }

        [HttpPost("EditUserPersonalInfo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserPersonalInfo(EditUserProfileViewModel model)
        {
            model.UserId = User.GetUserId();

            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            

            var result = await _userProfileServices.EditUserProfileAsync(model);
            switch (result)
            {
                case EditUserProfileResult.Success:
                    TempData["Message"] = "ProfileEditSuccess";
                    return RedirectToAction(nameof(Profile));
                case EditUserProfileResult.UserNotFound:
                    ModelState.AddModelError(nameof(EditUserProfileViewModel.NationalCode), "کاربر یاقت نشد");
                    return View(model);
                case EditUserProfileResult.Error:
                    TempData["Massage"] = "FailedEditProfile";
                    return View(model);
            }

            ViewData["Current-Page"] = "UserPersonalInfo";
            return View();
        }
        #endregion

    }
}
