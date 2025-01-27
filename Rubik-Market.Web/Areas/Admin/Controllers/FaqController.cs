using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.FAQ;

namespace Rubik_Market.Web.Areas.Admin.Controllers
{
    public class FaqController : AdminBaseController
    {
        #region Constructor

        private readonly IFaqServices _faqServices;

        public FaqController(IFaqServices faqServices)
        {
            _faqServices = faqServices;
        }

        #endregion

        #region List

        public async Task<IActionResult> List()
        {
            var model = await _faqServices.GetFaqAdminSideAsync();
            return View(model);
        }

        public async Task<IActionResult> DeletedList()
        {
            var model = await _faqServices.GetDeletedFaqAdminSideAsync();
            return View(model);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult CreateFaq()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaq(CreateFaqViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion
            var result = await _faqServices.CreateFaqAsync(model);
            switch (result)
            {
                case CreateFaqResult.Success:
                    TempData[SuccessMessage] = "سوال جدید با موفقیت اضافه شد";
                    return RedirectToAction(nameof(List));
                case CreateFaqResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return View(model);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> EditFaq(int id)
        {
            var faq = await _faqServices.GetFaqForEdit(id);
            if (faq == null)
                return NotFound();

            return View(faq);
            
        }

        [HttpPost]
        public async Task<IActionResult> EditFaq(UpdateFaqViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion
            var result = await _faqServices.UpdateFaqAsync(model);
            switch (result)
            {
                case UpdateFaqResult.Success:
                    TempData[SuccessMessage] = "سوال با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List));
                case UpdateFaqResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return View(model);
        }
        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> DeleteFaq(int id)
        {
            var result = await _faqServices.DeleteFaqAsync(id);
            switch (result)
            {
                case DeleteFaqResult.Success:
                    TempData[SuccessMessage] = "سوال با موفقیت حذف شد";
                    return RedirectToAction(nameof(List));
                case DeleteFaqResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
