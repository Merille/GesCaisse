using EasytransitCaisse.Data;
using EasytransitCaisse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasytransitCaisse.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(
            DateTime? dateDebut,
            DateTime? dateFin,
            string? statutJournee,
            int? caisseId)
        {
            // ==========================
            // INITIALISATION DES FILTRES
            // ==========================

            dateDebut ??= DateTime.Today;
            dateFin ??= DateTime.Today;

            DashboardViewModel model = new DashboardViewModel
            {
                DateDebut = dateDebut,
                DateFin = dateFin,
                StatutJournee = statutJournee,
                CaisseId = caisseId
            };

            // ==========================
            // LISTE DES CAISSES
            // ==========================

            model.ListeCaisses = _context.Caisses
                .OrderBy(c => c.ChkDescription)
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),   // ou c.ID
                    Text = c.ChkCode + " - " + c.ChkDescription
                })
                .ToList();

            model.ListeCaisses.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "-- Toutes les caisses --"
            });

            // ==========================
            // JOURNEES
            // ==========================

            var journees = _context.JourneesCaisses
                .Include(j => j.Caisse)
                .AsQueryable();

            journees = journees.Where(j =>
                j.DateJournee.Date >= dateDebut.Value.Date &&
                j.DateJournee.Date <= dateFin.Value.Date);

            if (!string.IsNullOrWhiteSpace(statutJournee))
            {
                journees = journees.Where(j => j.Statut == statutJournee);
            }

            if (caisseId.HasValue)
            {
                journees = journees.Where(j => j.CaisseId == caisseId.Value);
            }

            // ==========================
            // OPERATIONS
            // ==========================

            var operations = _context.OperationsCaisses
                .Include(o => o.Utilisateur)
                .Include(o => o.JourneeCaisse)
                .AsQueryable();

            operations = operations.Where(o =>
                o.DateOperation.Date >= dateDebut.Value.Date &&
                o.DateOperation.Date <= dateFin.Value.Date);

            if (!string.IsNullOrWhiteSpace(statutJournee))
            {
                operations = operations.Where(o =>
                    o.JourneeCaisse.Statut == statutJournee);
            }

            if (caisseId.HasValue)
            {
                operations = operations.Where(o =>
                 o.JourneeCaisse.CaisseId == caisseId.Value);

            }

            // ==========================
            // INDICATEURS
            // ==========================

            model.NombreUtilisateurs = _context.Utilisateurs.Count();

            model.NombreCaissesOuvertes = _context.JourneesCaisses
                .Count(j => j.Statut == "Ouverte");

            model.NombreOperations = operations.Count();

            model.NombreEncaissements = operations
                .Count(o => o.TypeOperation == "Encaissement");

            model.NombreDecaissements = operations
                .Count(o => o.TypeOperation == "Décaissement");

            // ==========================
            // MONTANTS
            // ==========================

            model.EntreesJour = operations
                .Where(o => o.TypeOperation == "Encaissement")
                .Sum(o => (decimal?)o.Montant) ?? 0;

            model.SortiesJour = operations
                .Where(o => o.TypeOperation == "Décaissement")
                .Sum(o => (decimal?)o.Montant) ?? 0;

            

            // ==========================
            // SOLDE INITIAL DES CAISSES
            // ==========================

            model.SoldeCaisse = journees
                .Sum(j => (decimal?)j.SoldeInitial) ?? 0;

            model.SoldeJour = model.SoldeCaisse+ model.EntreesJour - model.SortiesJour;

            // ==========================
            // TOTAUX
            // ==========================

            model.TotalEncaissementsMois = model.EntreesJour;

            model.TotalDecaissementsMois = model.SortiesJour;

            // ==========================
            // DERNIERES OPERATIONS
            // ==========================

            model.DernieresOperations = operations
                .OrderByDescending(o => o.DateOperation)
                .Take(10)
                .Select(o => new OperationDashboardViewModel
                {
                    DateOperation = o.DateOperation,
                    Reference = "OP" + o.Id.ToString("000000"),
                    Libelle = o.Libelle,
                    TypeOperation = o.TypeOperation,
                    Montant = o.Montant,
                    Caissier = o.Utilisateur.NomComplet
                })
                .ToList();

            return View(model);
        }
    }
}