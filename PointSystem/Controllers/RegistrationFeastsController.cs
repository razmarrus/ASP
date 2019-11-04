﻿using System;
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
    public class RegistrationFeastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationFeastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegistrationFeasts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RegistrationFeasts.Include(r => r.AspNetUser).Include(r => r.Feast);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistrationFeasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrationFeast = await _context.RegistrationFeasts
                .Include(r => r.AspNetUser)
                .Include(r => r.Feast)
                .FirstOrDefaultAsync(m => m.id == id);
            if (registrationFeast == null)
            {
                return NotFound();
            }

            return View(registrationFeast);
        }

        // GET: RegistrationFeasts/Create
        //[Authorize(Roles = "admin")]
        public IActionResult Create(int id)
        {
            ViewBag.Fid = id;
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id");
            //ViewData["Topic"] = new SelectList(_context.Feasts, "Topic", "Topic");
            return View();
        }

        // POST: RegistrationFeasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,StartTime,EndTime,Content,Point,Status,AspNetUserId,FeastId")] RegistrationFeast registrationFeast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrationFeast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
            ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id", registrationFeast.FeastId);
            return View(registrationFeast);
        }

 /*       public async Task<IActionResult> Follow(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrationFeast = await _context.RegistrationFeasts.FindAsync(id);
            if (registrationFeast == null)
            {
                return NotFound();
            }

            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
            ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id", registrationFeast.FeastId);
            //return View(registrationFeast);
            return "Your following, " + order.User + ", !";
        }
*/

        //[Authorize(Roles = "admin")]
        // GET: RegistrationFeasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrationFeast = await _context.RegistrationFeasts.FindAsync(id);
            if (registrationFeast == null)
            {
                return NotFound();
            }
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
            ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id", registrationFeast.FeastId);
            return View(registrationFeast);
        }

        // POST: RegistrationFeasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id,StartTime,EndTime,Content,Point,Status,AspNetUserId,FeastId")] RegistrationFeast registrationFeast)
        {
            if (id != registrationFeast.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrationFeast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationFeastExists(registrationFeast.id))
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
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
            ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id", registrationFeast.FeastId);
            return View(registrationFeast);
        }

        // GET: RegistrationFeasts/Delete/5
        //        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrationFeast = await _context.RegistrationFeasts
                .Include(r => r.AspNetUser)
                .Include(r => r.Feast)
                .FirstOrDefaultAsync(m => m.id == id);
            if (registrationFeast == null)
            {
                return NotFound();
            }

            return View(registrationFeast);
        }

        // POST: RegistrationFeasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrationFeast = await _context.RegistrationFeasts.FindAsync(id);
            _context.RegistrationFeasts.Remove(registrationFeast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationFeastExists(int id)
        {
            return _context.RegistrationFeasts.Any(e => e.id == id);
        }
    }
}