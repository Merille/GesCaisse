using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace EasytransitCaisse.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // HASH PASSWORD
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var hash = HashPassword(model.Password);

            var user = _context.Utilisateurs
                .FirstOrDefault(u =>
                    u.NomUtilisateur == model.Username &&
                    u.MotPasse == hash);

            if (user == null)
            {
                ViewBag.Error = "Utilisateur ou mot de passe incorrect";
                return View();
            }

            // SESSION
            HttpContext.Session.SetString("User", user.NomUtilisateur);
            HttpContext.Session.SetString("Profil", user.Profil);

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}