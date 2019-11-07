using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointSystem.Data;
using PointSystem.Models;

namespace PointSystem.Controllers
{
    public class FeastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feasts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feasts.ToListAsync());
        }

        // GET: Feasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feast = await _context.Feasts
                .FirstOrDefaultAsync(m => m.id == id);
            if (feast == null)
            {
                return NotFound();
            }

            return View(feast);
        }

        // GET: Feasts/Create

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,StartTime,EndTime,Topic,Content,Location")] Feast feast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feast);
        }

        // GET: Feasts/Edit/5

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feast = await _context.Feasts.FindAsync(id);
            if (feast == null)
            {
                return NotFound();
            }
            return View(feast);
        }

        // POST: Feasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,StartTime,EndTime,Topic,Content,Location")] Feast feast)
        {
            if (id != feast.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeastExists(feast.id))
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
            return View(feast);
        }

        // GET: Feasts/Delete/5

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feast = await _context.Feasts
                .FirstOrDefaultAsync(m => m.id == id);
            if (feast == null)
            {
                return NotFound();
            }

            return View(feast);
        }

        // POST: Feasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feast = await _context.Feasts.FindAsync(id);
            _context.Feasts.Remove(feast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeastExists(int id)
        {
            return _context.Feasts.Any(e => e.id == id);
        }
    }
}
