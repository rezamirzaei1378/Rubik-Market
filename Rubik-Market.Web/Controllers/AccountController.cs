using Microsoft.AspNetCore.Mvc;

namespace Rubik_Market.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Register
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        #endregion

        #region Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        #endregion

    }
}
