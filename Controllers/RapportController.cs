using EasytransitCaisse.Controllers;
using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using EasytransitCaisse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;



namespace EasytransitCaisse.Controllers
{
    public class RapportController : Controller
    {
        private readonly AppDbContext _context;

        public RapportController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOperations(DateTime? dateDebut, DateTime? dateFin, string type, int? caisseId)
        {
            var query = _context.OperationsCaisses
                .Include(x => x.JourneeCaisse)
                .AsQueryable();

            if (dateDebut != null)
                query = query.Where(x => x.DateOperation >= dateDebut);

            if (dateFin != null)
                query = query.Where(x => x.DateOperation <= dateFin);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.TypeOperation == type);

            if (caisseId != null)
                query = query.Where(x => x.JourneeCaisse.CaisseId == caisseId);

            var data = query.Select(x => new
            {
                x.DateOperation,
                Caisse = x.JourneeCaisse.Caisse.ChkDescription,
                x.TypeOperation,
                x.Montant,
                x.Libelle
            }).ToList();

            return Json(data);
        }

        #region RAPPORT CAISSES
        public IActionResult Operations(DateTime? dateDebut,
                                   DateTime? dateFin,
                                   int? caisseId,
                                   string? typeOperation)
        {
            var query = _context.OperationsCaisses
                .Include(x => x.JourneeCaisse)
                .Include(x => x.Utilisateur)
                .Include(x => x.Client)
                .AsQueryable();

            if (dateDebut.HasValue)
                query = query.Where(x => x.DateOperation >= dateDebut.Value);

            if (dateFin.HasValue)
                query = query.Where(x => x.DateOperation <= dateFin.Value);

            if (caisseId.HasValue)
                query = query.Where(x => x.JourneeCaisse.CaisseId == caisseId);

            if (!string.IsNullOrWhiteSpace(typeOperation))
                query = query.Where(x => x.TypeOperation == typeOperation);

            var model = new RapportOperationsVM
            {
                DateDebut = dateDebut,
                DateFin = dateFin,
                CaisseId = caisseId,
                TypeOperation = typeOperation,
                Operations = query
                    .OrderByDescending(x => x.DateOperation)
                    .ToList()
            };

            model.TotalEntrees = model.Operations
                .Where(x => x.TypeOperation == "Encaissement")
                .Sum(x => x.Montant);

            model.TotalSorties = model.Operations
                .Where(x => x.TypeOperation == "Décaissement")
                .Sum(x => x.Montant);

            model.MontantInitialJournees = _context.JourneesCaisses
                .Where(j =>
                    (!dateDebut.HasValue || j.DateJournee >= dateDebut.Value) &&
                    (!dateFin.HasValue || j.DateJournee <= dateFin.Value) &&
                    (!caisseId.HasValue || j.CaisseId == caisseId.Value))
                .Sum(j => (decimal?)j.SoldeInitial) ?? 0;

            ViewBag.Caisses = new SelectList(_context.Caisses, "ID", "ChkDescription");

            return View(model);
        }

        public IActionResult PreviewOperations(DateTime? dateDebut,
                                       DateTime? dateFin,
                                       int? caisseId,
                                       string? typeOperation)
        {
            var query = _context.OperationsCaisses
                .Include(x => x.JourneeCaisse)
                .Include(x => x.Utilisateur)
                .Include(x => x.Client)
                .AsQueryable();

            if (dateDebut.HasValue)
                query = query.Where(x => x.DateOperation >= dateDebut);

            if (dateFin.HasValue)
                query = query.Where(x => x.DateOperation <= dateFin);

            if (caisseId.HasValue)
                query = query.Where(x => x.JourneeCaisse.CaisseId == caisseId);

            if (!string.IsNullOrEmpty(typeOperation))
                query = query.Where(x => x.TypeOperation == typeOperation);

            var model = new RapportOperationsVM
            {
                DateDebut = dateDebut,
                DateFin = dateFin,
                CaisseId = caisseId,
                TypeOperation = typeOperation,
                Operations = query.OrderByDescending(x => x.DateOperation).ToList()
            };

            model.TotalEntrees = model.Operations
                .Where(x => x.TypeOperation == "Encaissement")
                .Sum(x => x.Montant);

            model.TotalSorties = model.Operations
                .Where(x => x.TypeOperation == "Décaissement")
                .Sum(x => x.Montant);

            model.MontantInitialJournees = _context.JourneesCaisses
                .Where(j =>
                    (!dateDebut.HasValue || j.DateJournee >= dateDebut.Value) &&
                    (!dateFin.HasValue || j.DateJournee <= dateFin.Value) &&
                    (!caisseId.HasValue || j.CaisseId == caisseId.Value))
                .Sum(j => (decimal?)j.SoldeInitial) ?? 0;

            return View(model);
        }
        #endregion

        #region RAPPORT JOURNEES

        public IActionResult Journees(DateTime? dateDebut,
                                      DateTime? dateFin,
                                      int? caisseId,
                                      string? statut)
        {
            var query = _context.JourneesCaisses
                .Include(x => x.Caisse)
                .AsQueryable();

            if (dateDebut.HasValue)
                query = query.Where(x => x.DateJournee >= dateDebut.Value);

            if (dateFin.HasValue)
                query = query.Where(x => x.DateJournee <= dateFin.Value);

            if (caisseId.HasValue)
                query = query.Where(x => x.CaisseId == caisseId);

            if (!string.IsNullOrWhiteSpace(statut))
                query = query.Where(x => x.Statut == statut);

            var model = new RapportJourneesVM
            {
                DateDebut = dateDebut,
                DateFin = dateFin,
                CaisseId = caisseId,
                Statut = statut,
                Journees = query
                    .OrderByDescending(x => x.DateJournee)
                    .ToList()
            };

            ViewBag.Caisses = new SelectList(_context.Caisses, "ID", "ChkDescription");

            return View(model);
        }

        #endregion

        #region RAPPORT CAISSES

        public IActionResult Caisses(string? recherche)
        {
            var query = _context.Caisses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(recherche))
            {
                query = query.Where(x =>
                    x.ChkCode.Contains(recherche) ||
                    x.ChkDescription.Contains(recherche));
            }

            var model = new RapportCaissesVM
            {
                Recherche = recherche,
                Caisses = query
                    .OrderBy(x => x.ChkDescription)
                    .ToList()
            };

            return View(model);
        }

        #endregion

    }
}