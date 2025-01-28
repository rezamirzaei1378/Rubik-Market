using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Domain.ViewModels.Address;
using System.Collections.Generic;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Areas.UserPanel.Controllers
{
    public class UserAddressController : UserBaseController
    {
        #region Constructor

        private readonly IUserAddressServices _userAddressServices;

        public UserAddressController(IUserAddressServices userAddressServices)
        {
            _userAddressServices = userAddressServices;
        }

        #endregion


        #region List

        [HttpGet("Address")]
        public IActionResult List()
        {
            return View();
        }

        #endregion

        #region AddAddress

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddAddressViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return ViewComponent("AddUserAddress",model);
            }

            #endregion

            var result = await _userAddressServices.AddAddressAsync(model);
            switch (result)
            {
                case AddAddressResult.Success:
                    TempData[SuccessMessage] = "ثبت آدرس با موفقیت انجام شد";
                    break;
                case AddAddressResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case AddAddressResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر یافت نشد";
                    break;

            }
            return RedirectToAction(nameof(List));

        }

        #endregion

        #region EditAddress

        [HttpGet("Edit-Address")]
        public async Task<IActionResult> EditAddress(int id)
        {
            var address = await _userAddressServices.GetAddressForEditAsync(id);
            if (address == null)
            {
                return RedirectToAction(nameof(List));
            }
            return View(address);
        }

        [HttpPost("Edit-Address")]
        public async Task<IActionResult> EditAddress(EditAddressViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _userAddressServices.EditAddressAsync(model);

            switch (result)
            {
                case EditAddressResult.Success:
                    TempData[SuccessMessage] = "ویرایش آدرس با موفقیت انجام شد";
                    return RedirectToAction(nameof(List));
                case EditAddressResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case EditAddressResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر یافت نشد";
                    break;
                case EditAddressResult.AddressNotFound:
                    TempData[ErrorMessage] = "آدرس یافت نشد";
                    break;
            }

            return View(model);
        }

        #endregion

        #region DeleteAddress

        public async Task<IActionResult> DeleteAddress(int id)
        {
            var result = await _userAddressServices.DeleteAddressAsync(id);

            switch (result)
            {
                case DeleteAddressResult.Success:
                    TempData[SuccessMessage] = " آدرس با موفقیت حذف شد";
                    break;
                case DeleteAddressResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case DeleteAddressResult.AddressNotFound:
                    TempData[ErrorMessage] = "آدرس یافت نشد";
                    break;
            }

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region ChangeCurrentAddress

        public async Task<IActionResult> ChangeCurrentAddress(int id)
        {
            var result = await _userAddressServices.ChangeCurrentAddressAsync(id);

            switch (result)
            {
                case ChangeCurrentAddressResult.Success:
                    TempData[SuccessMessage] = " آدرس با موفقیت حذف شد";
                    break;
                case ChangeCurrentAddressResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case ChangeCurrentAddressResult.AddressNotFound:
                    TempData[ErrorMessage] = "آدرس یافت نشد";
                    break;
            }

            return RedirectToAction("List");
        }

        #endregion
    }
}
