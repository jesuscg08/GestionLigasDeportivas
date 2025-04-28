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
    [Authorize(Policy = "BasicAccess")]
    public class EstadisticaController : Controller
    {
        private readonly LigaDeportivaContext _context;

        public EstadisticaController(LigaDeportivaContext context)
        {
            _context = context;
        }

        // GET: Estadistica
        public async Task<IActionResult> Index()
        {
            var ligaDeportivaContext = _context.Estadisticas.Include(e => e.Equipo).Include(e => e.Evento).Include(e => e.Jugador);
            return View(await ligaDeportivaContext.ToListAsync());
        }

        // GET: Estadistica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadistica = await _context.Estadisticas
                .Include(e => e.Equipo)
                .Include(e => e.Evento)
                .Include(e => e.Jugador)
                .FirstOrDefaultAsync(m => m.EstadisticaId == id);
            if (estadistica == null)
            {
                return NotFound();
            }

            return View(estadistica);
        }

        // GET: Estadistica/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId");
            ViewData["JugadorId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Estadistica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadisticaId,EventoId,JugadorId,EquipoId,Goles,Asistencias,TarjetasRojas,TarjetasAmarillas")] Estadistica estadistica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadistica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", estadistica.EquipoId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId", estadistica.EventoId);
            ViewData["JugadorId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", estadistica.JugadorId);
            return View(estadistica);
        }

        // GET: Estadistica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadistica = await _context.Estadisticas.FindAsync(id);
            if (estadistica == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", estadistica.EquipoId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId", estadistica.EventoId);
            ViewData["JugadorId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", estadistica.JugadorId);
            return View(estadistica);
        }

        // POST: Estadistica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadisticaId,EventoId,JugadorId,EquipoId,Goles,Asistencias,TarjetasRojas,TarjetasAmarillas")] Estadistica estadistica)
        {
            if (id != estadistica.EstadisticaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadistica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadisticaExists(estadistica.EstadisticaId))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", estadistica.EquipoId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId", estadistica.EventoId);
            ViewData["JugadorId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", estadistica.JugadorId);
            return View(estadistica);
        }

        // GET: Estadistica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadistica = await _context.Estadisticas
                .Include(e => e.Equipo)
                .Include(e => e.Evento)
                .Include(e => e.Jugador)
                .FirstOrDefaultAsync(m => m.EstadisticaId == id);
            if (estadistica == null)
            {
                return NotFound();
            }

            return View(estadistica);
        }

        // POST: Estadistica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadistica = await _context.Estadisticas.FindAsync(id);
            if (estadistica != null)
            {
                _context.Estadisticas.Remove(estadistica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadisticaExists(int id)
        {
            return _context.Estadisticas.Any(e => e.EstadisticaId == id);
        }
    }
}
