using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Controllers
{
    public class HomeController(IFaqServices faqServices) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("FAQ")]
        public async Task<IActionResult> Faq()
        {
            var model = await faqServices.GetFaqClientSideAsync();
            return View(model);
        }
    }
}
