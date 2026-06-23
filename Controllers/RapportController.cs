using EasytransitCaisse.Controllers;
using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Operations()
        {
            ViewBag.Caisses = _context.Caisses.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult GetOperations(DateTime? dateDebut, DateTime? dateFin, string type, int? caisseId)
        {
            var query = _context.OperationsCaisses.AsQueryable();

            if (dateDebut != null)
                query = query.Where(x => x.DateOperation >= dateDebut);

            if (dateFin != null)
                query = query.Where(x => x.DateOperation <= dateFin);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.TypeOperation == type);

            if (caisseId != null)
                query = query.Where(x => x.Id == caisseId);

            var data = query.Select(x => new
            {
                x.DateOperation,
                Caisse = x.Caisse.ChkDescription,
                x.TypeOperation,
                x.Montant,
                x.Libelle
            }).ToList();

            return Json(data);
        }
    }
}
