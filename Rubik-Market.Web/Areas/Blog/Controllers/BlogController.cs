using Microsoft.AspNetCore.Mvc;

namespace Rubik_Market.Web.Areas.Blog.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
