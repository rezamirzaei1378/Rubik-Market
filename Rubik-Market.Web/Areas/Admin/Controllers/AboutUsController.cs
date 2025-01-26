using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.AboutUs;

namespace Rubik_Market.Web.Areas.Admin.Controllers
{
    public class AboutUsController: AdminBaseController
    {
        #region Constructor

        private readonly IAboutUsServices _aboutUsServices;

        // ReSharper disable once ConvertToPrimaryConstructor
        public AboutUsController(IAboutUsServices aboutUsServices)
        {
            _aboutUsServices = aboutUsServices;
        }

        #endregion

        #region ListTeamMember

        public async Task<IActionResult> List()
        {
            //var aboutUs = services.GetAboutUsPageDetail();
            var model = await _aboutUsServices.GetAllTeamMembersAsync();
            return View(model);
        }

        #endregion

        #region AddAboutUsDesc

        [HttpGet]
        public IActionResult AddAboutUsDescription()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAboutUsDescription(AddAboutUsDescriptionViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion
            var result = await _aboutUsServices.AddAboutUsDescriptionAsync(model);
            switch (result)
            {
                case AddAboutUsDescriptionResult.Success:
                    TempData[SuccessMessage] = "درباره ما با موفقیت ثبت شد";
                    return RedirectToAction(nameof(List));
                case AddAboutUsDescriptionResult.Error:
                    ViewData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return View(model);
        }

        #endregion

        #region EditAboutUsDesc

        [HttpGet]
        public async Task<IActionResult> EditAboutUsDescription()
        {
            var aboutUs = await _aboutUsServices.GetAboutUsDescriptionForEditAsync();

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (aboutUs == null)
            {
                return RedirectToAction(nameof(AddAboutUsDescription));
            }
            return View(aboutUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAboutUsDescription(EditAboutUsDescriptionViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _aboutUsServices.UpdateAboutUsDescriptionAsync(model);
            switch (result)
            {
                case EditAboutUsDescriptionResult.Success:
                    ViewData[SuccessMessage] = "درباره ما با موفقت تغییر کرد";
                    return RedirectToAction(nameof(List));
                case EditAboutUsDescriptionResult.Error:
                    ViewData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }

            return View(model);
        }

        #endregion

        #region AddTeamMember

        [HttpGet]
        public IActionResult AddTeamMember()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamMember(AddTeamMemberViewModel model)
        {
            var result = await _aboutUsServices.AddTeamMemberAsync(model);
            switch (result)
            {
                case AddTeamMemberResult.Success:
                    TempData[SuccessMessage] = "عضو جدید ما با موفقیت ثبت شد";
                    return RedirectToAction(nameof(List));
                case AddTeamMemberResult.Error:
                    ViewData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return View(model);
        }

        #endregion

        #region EditTeamMember

        [HttpGet]
        public async Task<IActionResult> EditTeamMember(int id)
        {
            var aboutUs = await _aboutUsServices.GetTeamMemberForEditAsync(id);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (aboutUs == null)
            {
                return RedirectToAction(nameof(AddAboutUsDescription));
            }
            return View(aboutUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeamMember(EditTeamMemberViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _aboutUsServices.UpdateTeamMemberAsync(model);
            switch (result)
            {
                case EditTeamMemberResult.Success:
                    ViewData[SuccessMessage] = "مخشصات عضو تیم با موفقت تغییر کرد";
                    return RedirectToAction(nameof(List));
                case EditTeamMemberResult.Error:
                    ViewData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }

            return View(model);
        }

        #endregion

        #region DeleteTeamMember

        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var result = await _aboutUsServices.DeleteTeamMemberAsync(id);

            switch (result)
            {
                case DeleteTeamMemberResult.Success:
                    TempData[SuccessMessage] = "عضو تیم  با موفقیت حذف شد";
                    break;
                case DeleteTeamMemberResult.Error:
                    ViewData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
