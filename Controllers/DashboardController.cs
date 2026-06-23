using Microsoft.AspNetCore.Mvc;

namespace EasytransitCaisse.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
