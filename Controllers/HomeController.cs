using System.Diagnostics;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasytransitCaisse.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
}
}
