using EasytransitCaisse.Data;
using EasytransitCaisse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasytransitCaisse.Controllers
{
    // Gestion des sociétés (tenants) — réservée au SuperAdmin, portée globale.
    [Authorize(Policy = "SuperAdmin")]
    public class TenantController : Controller
    {
        private readonly AppDbContext _context;

        public TenantController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tenants = await _context.Tenants
                .OrderBy(t => t.Nom)
                .ToListAsync();

            return View(tenants);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tenant tenant)
        {
            if (!ModelState.IsValid)
                return View(tenant);

            _context.Tenants.Add(tenant);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var tenant = _context.Tenants.Find(id);

            if (tenant == null)
                return NotFound();

            return View(tenant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tenant tenant)
        {
            var existing = _context.Tenants.Find(tenant.Id);

            if (existing == null)
                return NotFound();

            existing.Nom = tenant.Nom;
            existing.Code = tenant.Code;
            existing.Actif = tenant.Actif;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
