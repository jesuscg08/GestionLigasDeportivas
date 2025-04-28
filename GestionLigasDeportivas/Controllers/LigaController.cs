
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionLigasDeportivas.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionLigasDeportivas.Controllers
{
    [Authorize(Policy = "FullAccess")]
    public class LigaController : Controller
    {
        private readonly LigaDeportivaContext _context;

        public LigaController(LigaDeportivaContext context)
        {
            _context = context;
        }

        // GET: Liga
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ligas.ToListAsync());
        }

        // GET: Liga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Ligas
                .FirstOrDefaultAsync(m => m.LigaId == id);
            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }

        // GET: Liga/Create
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Liga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("LigaId,Nombre,FechaInicio,FechaFin,NumeroJornadas")] Liga liga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(liga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(liga);
        }

        // GET: Liga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Ligas.FindAsync(id);
            if (liga == null)
            {
                return NotFound();
            }
            return View(liga);
        }

        // POST: Liga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LigaId,Nombre,FechaInicio,FechaFin,NumeroJornadas")] Liga liga)
        {
            if (id != liga.LigaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(liga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LigaExists(liga.LigaId))
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
            return View(liga);
        }

        // GET: Liga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liga = await _context.Ligas
                .FirstOrDefaultAsync(m => m.LigaId == id);
            if (liga == null)
            {
                return NotFound();
            }

            return View(liga);
        }

        // POST: Liga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var liga = await _context.Ligas.FindAsync(id);
            if (liga != null)
            {
                _context.Ligas.Remove(liga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LigaExists(int id)
        {
            return _context.Ligas.Any(e => e.LigaId == id);
        }
    }
}
