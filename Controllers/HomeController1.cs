using Microsoft.AspNetCore.Mvc;

namespace EasytransitCaisse.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
