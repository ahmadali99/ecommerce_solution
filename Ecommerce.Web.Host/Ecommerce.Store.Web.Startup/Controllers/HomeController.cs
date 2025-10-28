using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Store.Web.Startup.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to Modular ASP.NET Core MVC!";
            return View();
        }
    }
}
