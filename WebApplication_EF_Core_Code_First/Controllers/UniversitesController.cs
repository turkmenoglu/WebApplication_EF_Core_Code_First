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
    public class UniversitesController : Controller
    {
        private readonly BloggingContext _context;

        public UniversitesController(BloggingContext context)
        {
            _context = context;
        }

        // GET: Universites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Universiteler.ToListAsync());
        }

        // GET: Universites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universite = await _context.Universiteler
                .FirstOrDefaultAsync(m => m.UniversiteId == id);
            if (universite == null)
            {
                return NotFound();
            }

            return View(universite);
        }

        // GET: Universites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Universites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UniversiteId,UniversiteAdi")] Universite universite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(universite);
        }

        // GET: Universites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universite = await _context.Universiteler.FindAsync(id);
            if (universite == null)
            {
                return NotFound();
            }
            return View(universite);
        }

        // POST: Universites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UniversiteId,UniversiteAdi")] Universite universite)
        {
            if (id != universite.UniversiteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversiteExists(universite.UniversiteId))
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
            return View(universite);
        }

        // GET: Universites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universite = await _context.Universiteler
                .FirstOrDefaultAsync(m => m.UniversiteId == id);
            if (universite == null)
            {
                return NotFound();
            }

            return View(universite);
        }

        // POST: Universites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var universite = await _context.Universiteler.FindAsync(id);
            _context.Universiteler.Remove(universite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversiteExists(int id)
        {
            return _context.Universiteler.Any(e => e.UniversiteId == id);
        }
    }
}
