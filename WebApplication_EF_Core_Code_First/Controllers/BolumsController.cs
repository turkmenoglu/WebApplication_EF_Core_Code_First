using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_EF_Core_Code_First.Models;

namespace WebApplication_EF_Core_Code_First.Controllers
{
    public class BolumsController : Controller
    {
        private readonly BloggingContext _context;

        public BolumsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: Bolums
        public async Task<IActionResult> Index()
        {
            var bloggingContext = _context.Bolumler.Include(b => b.Universite);
            return View(await bloggingContext.ToListAsync());
        }

        // GET: Bolums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolumler
                .Include(b => b.Universite)
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolum == null)
            {
                return NotFound();
            }

            return View(bolum);
        }

        // GET: Bolums/Create
        public IActionResult Create()
        {
            ViewData["UniversiteId"] = new SelectList(_context.Universiteler, "UniversiteId", "UniversiteId");
            return View();
        }

        // POST: Bolums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BolumId,BolumAdi,Aciklama,UniversiteId")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UniversiteId"] = new SelectList(_context.Universiteler, "UniversiteId", "UniversiteId", bolum.UniversiteId);
            return View(bolum);
        }

        // GET: Bolums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolumler.FindAsync(id);
            if (bolum == null)
            {
                return NotFound();
            }
            ViewData["UniversiteId"] = new SelectList(_context.Universiteler, "UniversiteId", "UniversiteId", bolum.UniversiteId);
            return View(bolum);
        }

        // POST: Bolums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BolumId,BolumAdi,Aciklama,UniversiteId")] Bolum bolum)
        {
            if (id != bolum.BolumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolumExists(bolum.BolumId))
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
            ViewData["UniversiteId"] = new SelectList(_context.Universiteler, "UniversiteId", "UniversiteId", bolum.UniversiteId);
            return View(bolum);
        }

        // GET: Bolums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolumler
                .Include(b => b.Universite)
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolum == null)
            {
                return NotFound();
            }

            return View(bolum);
        }

        // POST: Bolums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolum = await _context.Bolumler.FindAsync(id);
            _context.Bolumler.Remove(bolum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolumExists(int id)
        {
            return _context.Bolumler.Any(e => e.BolumId == id);
        }
    }
}
