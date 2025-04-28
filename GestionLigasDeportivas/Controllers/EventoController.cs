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
    public class EventoController : Controller
    {
        private readonly LigaDeportivaContext _context;

        public EventoController(LigaDeportivaContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var ligaDeportivaContext = _context.Eventos.Include(e => e.EquipoLocal).Include(e => e.EquipoVisitante);
            return View(await ligaDeportivaContext.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.EquipoLocal)
                .Include(e => e.EquipoVisitante)
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            ViewData["EquipoVisitanteId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            return View();
        }

        // POST: Evento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Nombre,Fecha,Hora,EquipoLocalId,EquipoVisitanteId,Notificacion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", evento.EquipoLocalId);
            ViewData["EquipoVisitanteId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", evento.EquipoVisitanteId);
            return View(evento);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", evento.EquipoLocalId);
            ViewData["EquipoVisitanteId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", evento.EquipoVisitanteId);
            return View(evento);
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Nombre,Fecha,Hora,EquipoLocalId,EquipoVisitanteId,Notificacion")] Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoId))
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
            ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", evento.EquipoLocalId);
            ViewData["EquipoVisitanteId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", evento.EquipoVisitanteId);
            return View(evento);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.EquipoLocal)
                .Include(e => e.EquipoVisitante)
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
