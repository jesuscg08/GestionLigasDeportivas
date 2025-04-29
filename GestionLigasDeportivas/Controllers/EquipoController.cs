using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionLigasDeportivas.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionLigasDeportivas.Controllers
{
    [Authorize(Policy = "MediumAccess")]
    public class EquipoController : Controller
    {
        private readonly LigaDeportivaContext _context;

        public EquipoController(LigaDeportivaContext context)
        {
            _context = context;
        }

        // GET: Equipo
        public async Task<IActionResult> Index()
        {
            var ligaDeportivaContext = _context.Equipos.Include(e => e.Liga);
            return View(await ligaDeportivaContext.ToListAsync());
        }

        // GET: Equipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.Liga)
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipo/Create
        [Authorize(Policy = "FullAccess")]
        public IActionResult Create()
        {
            ViewData["LigaId"] = new SelectList(_context.Ligas, "LigaId", "Nombre");
            return View();
        }

        // POST: Equipo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Create([Bind("EquipoId,Nombre,LigaId")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LigaId"] = new SelectList(_context.Ligas, "LigaId", "Nombre", equipo.LigaId);
            return View(equipo);
        }

        // GET: Equipo/Edit/5
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            ViewData["LigaId"] = new SelectList(_context.Ligas, "LigaId", "Nombre", equipo.LigaId);
            return View(equipo);
        }

        // POST: Equipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoId,Nombre,LigaId")] Equipo equipo)
        {
            if (id != equipo.EquipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.EquipoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LigaId"] = new SelectList(_context.Ligas, "LigaId", "Nombre", equipo.LigaId);
            return View(equipo);
        }

        // GET: Equipo/Delete/5
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.Liga)
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.EquipoId == id);
        }
    }
}
