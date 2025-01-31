using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.ContactUs;

namespace Rubik_Market.Web.Controllers
{
    public class ContactUsController : SiteBaseController
    {
        #region Constructor

        private readonly IContactUsServices _contactUsServices;

        public ContactUsController(IContactUsServices contactUsServices)
        {
            _contactUsServices = contactUsServices;
        }

        #endregion

        [HttpGet("Contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost("Contact-us")]
        public async Task<IActionResult> ContactUs(CreateContactUsViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _contactUsServices.CreateContactUsAsync(model);

            switch (result)
            {
                case CreateContactUsResult.Success:
                    TempData[SuccessMessage] = "پیام شما با موفقیت ارسا شد پاسخ از طریق ایمیل برای شما ارسال میشود";
                    break;
                case CreateContactUsResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return RedirectToAction(nameof(ContactUs));
        }
    }
}
