using Microsoft.AspNetCore.Mvc;

namespace Rubik_Market.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
