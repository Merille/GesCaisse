using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace EasytransitCaisse.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly AppDbContext _context;

        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        // LISTE
        public IActionResult Index()
        {
            var users = _context.Utilisateurs.ToList();
            return View(users);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Utilisateur user)
        {
            if (!ModelState.IsValid)
                return View(user);

            user.MotPasse = HashPassword(user.MotPasse);

            _context.Utilisateurs.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var user = _context.Utilisateurs.Find(id);

            if (user == null)
                return NotFound();

            user.MotPasse = ""; // ne pas afficher le hash

            return View(user);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Utilisateur user)
        {
            var existing = _context.Utilisateurs.Find(user.Id);

            if (existing == null)
                return NotFound();

            existing.NomUtilisateur = user.NomUtilisateur;
            existing.NomComplet = user.NomComplet;
            existing.Profil = user.Profil;

            // si mot de passe rempli → update
            if (!string.IsNullOrEmpty(user.MotPasse))
            {
                existing.MotPasse = HashPassword(user.MotPasse);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var user = _context.Utilisateurs.Find(id);

            if (user != null)
            {
                _context.Utilisateurs.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // HASH PASSWORD
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}