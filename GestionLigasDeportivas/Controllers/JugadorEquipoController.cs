using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionLigasDeportivas.Models;

namespace GestionLigasDeportivas.Controllers
{
    public class JugadorEquipoController : Controller
    {
        private readonly LigaDeportivaContext _context;

        public JugadorEquipoController(LigaDeportivaContext context)
        {
            _context = context;
        }

        // GET: JugadorEquipo
        public async Task<IActionResult> Index()
        {
            var ligaDeportivaContext = _context.JugadorEquipos.Include(j => j.Equipo).Include(j => j.Usuario);
            return View(await ligaDeportivaContext.ToListAsync());
        }

        // GET: JugadorEquipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugadorEquipo = await _context.JugadorEquipos
                .Include(j => j.Equipo)
                .Include(j => j.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugadorEquipo == null)
            {
                return NotFound();
            }

            return View(jugadorEquipo);
        }

        // GET: JugadorEquipo/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: JugadorEquipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,EquipoId")] JugadorEquipo jugadorEquipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jugadorEquipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", jugadorEquipo.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", jugadorEquipo.UsuarioId);
            return View(jugadorEquipo);
        }

        // GET: JugadorEquipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugadorEquipo = await _context.JugadorEquipos.FindAsync(id);
            if (jugadorEquipo == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", jugadorEquipo.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", jugadorEquipo.UsuarioId);
            return View(jugadorEquipo);
        }

        // POST: JugadorEquipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,EquipoId")] JugadorEquipo jugadorEquipo)
        {
            if (id != jugadorEquipo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugadorEquipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorEquipoExists(jugadorEquipo.Id))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", jugadorEquipo.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", jugadorEquipo.UsuarioId);
            return View(jugadorEquipo);
        }

        // GET: JugadorEquipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugadorEquipo = await _context.JugadorEquipos
                .Include(j => j.Equipo)
                .Include(j => j.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugadorEquipo == null)
            {
                return NotFound();
            }

            return View(jugadorEquipo);
        }

        // POST: JugadorEquipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugadorEquipo = await _context.JugadorEquipos.FindAsync(id);
            if (jugadorEquipo != null)
            {
                _context.JugadorEquipos.Remove(jugadorEquipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorEquipoExists(int id)
        {
            return _context.JugadorEquipos.Any(e => e.Id == id);
        }
    }
}
