using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.Admin.User;
using Rubik_Market.Domain.ViewModels.User.Areas;

namespace Rubik_Market.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {


        #region Constructor
        private readonly IUserServices _userServices;
        private readonly IUserProfileServices _userProfileServices;

        public UserController(IUserServices userServices, IUserProfileServices userProfileServices)
        {
            _userServices = userServices;
            _userProfileServices = userProfileServices;
        }

        #endregion

        #region UserList

        public async Task<IActionResult> List()
        {
            var users = await _userServices.GetAllUsersAsync();
            return View(users);
        }

        #endregion

        #region DeletedUserList

        public async Task<IActionResult> DeletedUserList()
        {
            var users = await _userServices.GetAllDeletedUsersAsync();
            return View(users);
        }

        #endregion

        #region CreateUser

        [HttpGet("Create-User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create-User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userServices.CreateUserAsync(model);
            switch (result)
            {
                case CreateUserResult.Success:
                    TempData[SuccessMessage] = "کاربر با موفقیت اضافه شد";
                    return RedirectToAction(nameof(List));
                case CreateUserResult.UserExist:
                    ModelState.AddModelError(nameof(CreateUserViewModel.UserEmail), "کاربری با این ایمیل موجود است");
                    return View(model);
                case CreateUserResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    return View(model);
            }
            return View(model);
        }

        #endregion

        #region EditUser

        [HttpGet("Edit-User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userExist = await _userServices.IsUserExistByIdAsync(id);
            if (!userExist)
            {
                TempData[ErrorMessage] = "کاربری یافت نشد";
                return RedirectToAction(nameof(List));
            }


            var model = await _userServices.GetUserByIdForEditAsync(id);

            return View(model);
        }


        [HttpPost("Edit-User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userServices.UpdateUserAsync(model);

            switch (result)
            {
                case EditUserResult.Success:
                    TempData[SuccessMessage] = "کاربر با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List));
                case EditUserResult.UserNotExist:
                    TempData[ErrorMessage] = "کاربر یافت نشد";
                    return View(model);
                case EditUserResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    return View(model);
            }

            return View(model);
        }

        #endregion

        #region Delete User

        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userServices.DeleteUserAsync(id);
            switch (result)
            {
                case DeleteUserResult.Success:
                    TempData[SuccessMessage] = "کاربر با موفقیت حذف شد";
                    return RedirectToAction(nameof(List));
                case DeleteUserResult.UserNotExist:
                    TempData[ErrorMessage] = "کاربر یافت نشد";
                    return RedirectToAction(nameof(List));
                case DeleteUserResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    return RedirectToAction(nameof(List));
            }
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region UserProfile

        public async Task<IActionResult> UserProfile(int userId)
        {
            var model = await _userProfileServices.GetUserProfileAsync(userId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        #endregion

        #region User Detailes Add

        [HttpGet]
        public IActionResult UserAddProfile(int userId)
        {
            var model = new AddUserProfileViewModel
            {
                UserId = userId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserAddProfile(AddUserProfileViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userProfileServices.AddUserProfileAsync(model);

            switch (result)
            {
                case AddUserProfileResult.Success:
                    TempData[SuccessMessage] = "پروفایل با موفقیت اضافه شد";
                    return RedirectToAction(nameof(List));
                case AddUserProfileResult.UserNotFound:
                    return NotFound();
                case AddUserProfileResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    return View(model);
            }

            return View(model);
        }

        #endregion

        #region User Detaile Edit

        [HttpGet]
        public async Task<IActionResult> UserEditProfile(int userId)
        {
            var userProfile = await _userProfileServices.GetUserProfileAsync(userId);
            if (userProfile == null)
            {
                return NotFound();
            }
            AdminEditUserProfileViewModel model = new AdminEditUserProfileViewModel()
            {
                UserId = userId,
                NationalCode = userProfile.NationalCode,
                CellPhoneNumber = userProfile.CellPhoneNumber,
                HousePhoneNumber = userProfile.HousePhoneNumber,
                CardNumberForRejectMoney = userProfile.CardNumberForRejectMoney
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserEditProfile(AdminEditUserProfileViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await _userProfileServices.AdminEditUserProfileAsync(model);

            switch (result)
            {
                case EditUserProfileResult.Success:
                    TempData[SuccessMessage] = "پروفایل با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List));
                case EditUserProfileResult.UserNotFound:
                    return NotFound();
                case EditUserProfileResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    return View(model);
            }
            return View(model);
        }

        #endregion

        #region User Detaile Delete

        public async Task<IActionResult> UserDetailDelete(int id)
        {
            var result = await _userProfileServices.DeleteUserProfileAsync(id);

            switch (result)
            {
                case DeleteProfileResult.Success:
                    TempData[SuccessMessage] = "پروفایل با موفقیت حذف شد";
                    return RedirectToAction(nameof(List));
                case DeleteProfileResult.UserNotFound:
                    return NotFound();
                case DeleteProfileResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    return RedirectToAction(nameof(UserProfile),id);
            }
            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
