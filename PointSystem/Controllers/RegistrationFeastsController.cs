﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointSystem.Data;
using PointSystem.Models;
using Microsoft.AspNetCore.Rewrite;
using System.Net;
using System.Text;
using System.IO;

namespace PointSystem.Controllers
{
    public class RegistrationFeastsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Feast CurrentFeast = null;
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
            if (User.Identity.IsAuthenticated) //UnauthorizedResult
            {
                var FeastDbContext = _context.Feasts;
                foreach (Feast bufferFeast in FeastDbContext)
                {
                    if (bufferFeast.id == id)
                    {
                        ViewData["Url"] = bufferFeast.RediarectUrl;
                        CurrentFeast = bufferFeast;
                        break;
                    }
                }

                ViewBag.Fid = id;
                ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
                ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id");
                //ViewData["Topic"] = new SelectList(_context.Feasts, "Topic", "Topic");
                return View();
            }
            else return Unauthorized();
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
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                registrationFeast.AspNetUserId = currentUserID;

                _context.Add(registrationFeast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
            ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id", registrationFeast.FeastId);
            if(CurrentFeast.RediarectUrl != null)
            {
                string messege = "\"Feast.Content\": \"" + CurrentFeast.Content + "\",\n\"User\": \"" + registrationFeast.AspNetUser + "\",\n\"Content\": \"" + registrationFeast.Content + "\",\n\"StartTime\": \"" + registrationFeast.StartTime + "\",\n\"EndTime\": \"" + registrationFeast.EndTime + "\",\n\"Point\": \"" + registrationFeast.Point;
                HttpWebRequest request = null;
                Uri uri = new Uri(CurrentFeast.RediarectUrl);
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(messege);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/json";//"application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                request.UseDefaultCredentials = true;
                using (Stream writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            return View(registrationFeast);
        }


        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public async Task<IActionResult> Consider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var registrationFeast = await _context.RegistrationFeasts.FindAsync(id);
            var registrationFeast = await _context.RegistrationFeasts
                .Include(r => r.AspNetUser)
                .Include(r => r.Feast)
                .FirstOrDefaultAsync(m => m.id == id);

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
        public async Task<IActionResult> Consider(int id, [Bind("id,StartTime,EndTime,Content,Point,Status,AspNetUserId,FeastId")] RegistrationFeast registrationFeast)
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
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (registrationFeast.AspNetUserId == currentUserID)
            { 
                ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
                ViewData["FeastId"] = new SelectList(_context.Feasts, "id", "id", registrationFeast.FeastId);
                return View(registrationFeast);
            }
            else return Redirect("~/Error/ErrorNoPower");
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
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (registrationFeast.AspNetUserId == currentUserID)
                return View(registrationFeast);
            else return Redirect("~/Error/ErrorNoPower");
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
