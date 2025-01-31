using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.ContactUs;

namespace Rubik_Market.Web.Areas.Admin.Controllers
{
    public class ContactUsController : AdminBaseController
    {
        #region Constructor

        private readonly IContactUsServices _contactUsServices;

        public ContactUsController(IContactUsServices contactUsServices)
        {
            _contactUsServices = contactUsServices;
        }

        #endregion
        public async Task<IActionResult> List()
        {
            var model = await _contactUsServices.GetContactUsListAsync();
            return View(model);
        }

        #region Answer

        [HttpGet]
        public async Task<IActionResult> AnswerContactUs(int id)
        {
            var model = await _contactUsServices.GetContactUsBIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AnswerContactUs(ContactUsAnswerViewModel model)
        {

            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _contactUsServices.ContactUsAnswerResultAsync(model);

            switch (result)
            {
                case AnswerResult.Success:
                    TempData[SuccessMessage] = "پاسخ با موفقیت ثبت شد";
                    break;
                case AnswerResult.ContactUsNotFound:
                    TempData[ErrorMessage] = "پبام یافت نشد";
                    break;
                case AnswerResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case AnswerResult.AnswerIsNull:
                    TempData[ErrorMessage] = "پاسخی دربافت نشد";
                    return View(model);
            }

            return RedirectToAction(nameof(List));
        }

        #endregion
        }
}
