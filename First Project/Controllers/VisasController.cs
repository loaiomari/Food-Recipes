using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;

namespace First_Project.Controllers
{
    public class VisasController : Controller
    {
        private readonly ModelContext _context;

        public VisasController(ModelContext context)
        {
            _context = context;
        }

        // GET: Visas
        public async Task<IActionResult> Index()
        {
              return _context.Visas != null ? 
                          View(await _context.Visas.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Visas'  is null.");
        }

        // GET: Visas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Visas == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .FirstOrDefaultAsync(m => m.VisaId == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // GET: Visas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisaId,VisaNumber,Cvc,ExpirationDate,Balance")] Visa visa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visa);
        }

        // GET: Visas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Visas == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas.FindAsync(id);
            if (visa == null)
            {
                return NotFound();
            }
            return View(visa);
        }

        // POST: Visas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("VisaId,VisaNumber,Cvc,ExpirationDate,Balance")] Visa visa)
        {
            if (id != visa.VisaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaExists(visa.VisaId))
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
            return View(visa);
        }

        // GET: Visas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Visas == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .FirstOrDefaultAsync(m => m.VisaId == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // POST: Visas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Visas == null)
            {
                return Problem("Entity set 'ModelContext.Visas'  is null.");
            }
            var visa = await _context.Visas.FindAsync(id);
            if (visa != null)
            {
                _context.Visas.Remove(visa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisaExists(decimal id)
        {
          return (_context.Visas?.Any(e => e.VisaId == id)).GetValueOrDefault();
        }
    }
}
