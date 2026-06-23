using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasytransitCaisse.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        // LISTE
        public IActionResult Index()
        {
            return View(_context.Clients.ToList());
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Client client)
        {
            Console.WriteLine("POST CREATE EXECUTE");
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                Console.WriteLine(string.Join(",", errors));

                return View(client);
            }

            _context.Clients.Add(client);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null) return NotFound();

            return View(client);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Client model)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == model.Id);

            if (client == null)
                return NotFound();

            client.CodeClient = model.CodeClient;
            client.NomSociete = model.NomSociete;
            client.Adresse = model.Adresse;
            client.Telephone = model.Telephone;

            client.Email = model.Email ?? ""; // 🔥 évite NULL

            client.NomContact = model.NomContact;
            client.FonctionContact = model.FonctionContact;
            client.TelephoneContact = model.TelephoneContact;
            client.EmailContact = model.EmailContact;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var client = _context.Clients.Find(id);

            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}