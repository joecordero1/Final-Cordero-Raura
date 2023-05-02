using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Cordero_Raura.Data;
using Final_Cordero_Raura.Models;

namespace Final_Cordero_Raura.Controllers
{
    public class ResenasController : Controller
    {
        private readonly Final_Cordero_RauraContext _context;

        public ResenasController(Final_Cordero_RauraContext context)
        {
            _context = context;
        }

        // GET: Resenas
        public async Task<IActionResult> Index()
        {
              return _context.Resena != null ? 
                          View(await _context.Resena.ToListAsync()) :
                          Problem("Entity set 'Final_Cordero_RauraContext.Resena'  is null.");
        }

        // GET: Resenas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resena == null)
            {
                return NotFound();
            }

            var resena = await _context.Resena
                .FirstOrDefaultAsync(m => m.IdResena == id);
            if (resena == null)
            {
                return NotFound();
            }

            return View(resena);
        }

        // GET: Resenas/Create
        public IActionResult Create()
        {
            //Aqui modifico y meto la siguiente linea
            ViewBag.Peliculas = _context.Pelicula.Select(p => new SelectListItem { Value = p.IdPelicula.ToString(), Text = p.Nombre }).ToList();
            ///////////////
            return View();
        }

        // POST: Resenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResena,Titulo,Texto,IdPelicula")] Resena resena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resena);
        }

        // GET: Resenas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resena == null)
            {
                return NotFound();
            }

            var resena = await _context.Resena.FindAsync(id);
            if (resena == null)
            {
                return NotFound();
            }
            return View(resena);
        }

        // POST: Resenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResena,Titulo,Texto,IdPelicula")] Resena resena)
        {
            if (id != resena.IdResena)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResenaExists(resena.IdResena))
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
            return View(resena);
        }

        // GET: Resenas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resena == null)
            {
                return NotFound();
            }

            var resena = await _context.Resena
                .FirstOrDefaultAsync(m => m.IdResena == id);
            if (resena == null)
            {
                return NotFound();
            }

            return View(resena);
        }

        // POST: Resenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resena == null)
            {
                return Problem("Entity set 'Final_Cordero_RauraContext.Resena'  is null.");
            }
            var resena = await _context.Resena.FindAsync(id);
            if (resena != null)
            {
                _context.Resena.Remove(resena);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResenaExists(int id)
        {
          return (_context.Resena?.Any(e => e.IdResena == id)).GetValueOrDefault();
        }
    }
}
