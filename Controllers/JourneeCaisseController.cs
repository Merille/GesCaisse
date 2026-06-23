using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace EasytransitCaisse.Controllers
{
    public class JourneeCaisseController : Controller
    {
        private readonly AppDbContext _context;

        public JourneeCaisseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            var journee = _context.JourneesCaisses
                .FirstOrDefault(x => x.Id == id);

            if (journee == null)
                return NotFound();

            var encaisse = _context.OperationsCaisses
                .Where(x =>
                    x.JourneeCaisseId == id &&
                    x.TypeOperation == "Encaissement")
                .Sum(x => (decimal?)x.Montant) ?? 0;

            var decaisse = _context.OperationsCaisses
                .Where(x =>
                    x.JourneeCaisseId == id &&
                    x.TypeOperation == "Décaissement")
                .Sum(x => (decimal?)x.Montant) ?? 0;

            ViewBag.TotalEncaisse = encaisse;
            ViewBag.TotalDecaisse = decaisse;
            ViewBag.SoldeActuel =
                journee.SoldeInitial +
                encaisse -
                decaisse;

            ViewBag.Clients = _context.Clients
                .OrderBy(x => x.NomSociete)
                .ToList();

            ViewBag.Operations = _context.OperationsCaisses
                .Where(x => x.JourneeCaisseId == id)
                .OrderByDescending(x => x.DateOperation)
                .ToList();

            return View(journee);
        }
        [HttpPost]
        public IActionResult AjouterOperation(
        int journeeId,
        string typeOperation,
        decimal montant,
        string libelle,
        int? clientId)
        {
            var operation = new OperationCaisse
            {
                JourneeCaisseId = journeeId,
                DateOperation = DateTime.Now,
                TypeOperation = typeOperation,
                Montant = montant,
                Libelle = libelle,
                ClientId = clientId,
                UtilisateurId = 1
            };

            _context.OperationsCaisses.Add(operation);

            _context.SaveChanges();

            return RedirectToAction(
                "Details",
                new { id = journeeId });
        }
        public IActionResult Create(int id)
        {
            ViewBag.CaisseId = id;

            return View();
        }

        [HttpPost]
        public IActionResult Create(
    int caisseId,
    DateTime dateJournee,
    decimal soldeInitial)
        {
            var caisse = _context.Caisses
                .FirstOrDefault(x => x.ID == caisseId);

            if (caisse == null)
                return NotFound();

            // vérifier doublon
            var existe = _context.JourneesCaisses
                .Any(x =>
                    x.CaisseId == caisseId &&
                    x.DateJournee.Date == dateJournee.Date);

            if (existe)
            {
                TempData["Erreur"] =
                    "Cette journée existe déjà.";

                ViewBag.CaisseId = caisseId;

                return View();
            }

            var journee = new JourneeCaisse
            {
                CaisseId = caisseId,
                DateJournee = dateJournee,
                DateOuverture = DateTime.Now,
                SoldeInitial = soldeInitial,
                Statut = "Ouverte",

                // 🔥 ICI le numéro propre
                NumeroJournee = $"{caisse.ChkCode}-{dateJournee:yyyyMMdd}"
            };

            _context.JourneesCaisses.Add(journee);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = journee.Id });
        }

        [HttpPost]
        public IActionResult Cloturer(int id)
        {
            var journee = _context.JourneesCaisses
                .FirstOrDefault(x => x.Id == id);

            if (journee == null)
                return NotFound();

            journee.Statut = "Cloturee";
            journee.DateFermeture = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public IActionResult Supprimer(int id)
        {
            var journee = _context.JourneesCaisses
                .FirstOrDefault(x => x.Id == id);

            if (journee == null)
                return NotFound();

            // empêcher suppression si clôturée
            if (journee.Statut == "Cloturee")
            {
                TempData["Erreur"] =
                    "Impossible de supprimer une journée clôturée.";

                return RedirectToAction("Details", new { id });
            }

            _context.JourneesCaisses.Remove(journee);
            _context.SaveChanges();

            return RedirectToAction("Details", "Caisse", new { id = journee.CaisseId });
        }

        public IActionResult PrintJournees(int caisseId)
        {
            var caisse = _context.Caisses
                .FirstOrDefault(x => x.ID == caisseId);

            if (caisse == null)
            {
                return Content($"Aucune caisse trouvée pour l'ID {caisseId}");
            }


            var journees = _context.JourneesCaisses
                .Where(x => x.CaisseId == caisseId)
                .ToList();

            var model = journees.Select(j => new
            {
                j.DateJournee,
                j.SoldeInitial,

                Encaisse = _context.OperationsCaisses
                    .Where(o => o.JourneeCaisseId == j.Id &&
                                o.TypeOperation == "Encaissement")
                    .Sum(o => (decimal?)o.Montant) ?? 0,

                Decaisse = _context.OperationsCaisses
                    .Where(o => o.JourneeCaisseId == j.Id &&
                                o.TypeOperation == "Décaissement")
                    .Sum(o => (decimal?)o.Montant) ?? 0,

                Solde =
                    j.SoldeInitial +
                    (_context.OperationsCaisses
                        .Where(o => o.JourneeCaisseId == j.Id &&
                                    o.TypeOperation == "Encaissement")
                        .Sum(o => (decimal?)o.Montant) ?? 0)
                    -
                    (_context.OperationsCaisses
                        .Where(o => o.JourneeCaisseId == j.Id &&
                                    o.TypeOperation == "Décaissement")
                        .Sum(o => (decimal?)o.Montant) ?? 0),

                j.Statut
            }).ToList();

            ViewBag.CaisseName = caisse.ChkDescription + " - " + caisse.ChkCode;

            return new ViewAsPdf("PrintJournees", model)
            {
                FileName = "Journees.pdf"
            };
        }

        public IActionResult PrintOperations(int journeeId)
        {
            var ops = _context.OperationsCaisses
                .Where(x => x.JourneeCaisseId == journeeId)
                .ToList();

            return new ViewAsPdf("PrintOperations", ops)
            {
                FileName = "Operations_Caisse.pdf"
            };
        }

    }
}