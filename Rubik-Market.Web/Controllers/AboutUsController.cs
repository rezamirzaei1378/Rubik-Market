using Microsoft.AspNetCore.Mvc;

namespace Rubik_Market.Web.Controllers
{
    public class AboutUsController: Controller
    {
        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
