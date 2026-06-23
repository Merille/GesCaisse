using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace EasytransitCaisse.Controllers
{
    public class FactureController : Controller
    {
        private readonly AppDbContext _context;

        public FactureController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var factures = _context.Factures.ToList();
            return View(factures);
        }

        public IActionResult Create()
        {
            ViewBag.Clients = _context.Clients
                .OrderBy(x => x.NomSociete)
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Facture facture)
        {
            facture.DateFacture = DateTime.Now;
            facture.Statut = "Brouillon";

            _context.Factures.Add(facture);
            _context.SaveChanges();

            return RedirectToAction(
                "Edit",
                new { id = facture.Id });
        }

        public IActionResult Edit(int id)
        {
            var facture = _context.Factures
                .FirstOrDefault(f => f.Id == id);

            if (facture == null)
                return NotFound();

            ViewBag.Lignes = _context.LignesFactures
                .Where(x => x.FactureId == id)
                .ToList();

            return View(facture);
        }

        [HttpPost]
        public IActionResult AjouterLigne(
    int factureId,
    string designation,
    decimal quantite,
    decimal prixUnitaire)
        {
            var ligne = new LigneFacture
            {
                FactureId = factureId,
                Designation = designation,
                Quantite = quantite,
                PrixUnitaire = prixUnitaire,
                TotalLigne = quantite * prixUnitaire
            };

            _context.LignesFactures.Add(ligne);

            RecalculerFacture(factureId);

            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = factureId });
        }
        private void RecalculerFacture(int factureId)
        {
            var facture = _context.Factures
                .First(x => x.Id == factureId);

            var totalHt = _context.LignesFactures
                .Where(x => x.FactureId == factureId)
                .Sum(x => (decimal?)x.TotalLigne) ?? 0;

            facture.MontantHT = totalHt;

            facture.MontantTVA =
                totalHt * facture.TauxTVA / 100;

            facture.MontantTTC =
                facture.MontantHT +
                facture.MontantTVA;
        }

        [HttpPost]
        public IActionResult ValiderFacture(int factureId)
        {
            var facture = _context.Factures
                .First(x => x.Id == factureId);

            facture.Statut = "Validée";

            _context.SaveChanges();

            return RedirectToAction("Edit",
                new { id = factureId });
        }

        public IActionResult Print(int id)
        {
            var facture = _context.Factures
                .FirstOrDefault(x => x.Id == id);

            if (facture == null)
                return NotFound();

            var lignes = _context.LignesFactures
                .Where(x => x.FactureId == id)
                .ToList();

            ViewBag.Lignes = lignes;

            return new ViewAsPdf("Print", facture)
            {
                FileName = $"Facture_{facture.Numero}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 10, 10)
            };
        }

    }
}