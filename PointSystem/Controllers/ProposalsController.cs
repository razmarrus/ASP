using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointSystem.Data;
using PointSystem.Models;
//using Microsoft.AspNetCore.Identity.IdentityExtensions;
using System.Security.Claims;
//using ControllersApp.Util;
using Microsoft.AspNetCore.Http;


namespace PointSystem.Controllers
{
    public class ProposalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        UserManager<AspNetUser> _userManager;
  

        private Task<AspNetUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ProposalsController(ApplicationDbContext context) //, UserManager<AspNetUser> manager)
        {
            _context = context;
            //_userManager = manager;
        }

        
        // GET: Proposals
        public async Task<IActionResult> Index()
        {
            //_context.Users.Where(f => f.Email =="qwe").First();
            var applicationDbContext = _context.Proposals.Include(p => p.AspNetUser).Where(q => q.EndTime > DateTime.Now).OrderBy(t => t.EndTime);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _context.Proposals
                .Include(p => p.AspNetUser)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(m => m.id == id);

            if (proposal == null)
            {
                return NotFound();
            }

            return View(proposal);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id, string NewCommentary, string UserName)
        {
            var proposal = _context.Proposals
                .Include(p => p.AspNetUser)
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.id == id);

 

            //AddCommentary(project, NewCommentary, UserName);
            //_context.Projects.Update(project);
            //await _context.SaveChangesAsync();
            return View(proposal);
        }


        // GET: Proposals/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated) //UnauthorizedResult
            {
                //ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", registrationFeast.AspNetUserId);
                ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
                return View();
            }
            //else return View("~/Views/Wherever/SomeDir/MyView.aspx")
            else return new UnauthorizedResult();
            //else return new HtmlResult("<h2>Привет ASP.NET Core</h2>");
        }
 
        // POST: Proposals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,StartTime,EndTime,Topic,Content,Point,MaxPeople,Status,AspNetUserId")] Proposal proposal)
        {

            if(proposal.EndTime < proposal.StartTime)
            {
                ModelState.AddModelError("EndTime", "time is invalid");
                ModelState.AddModelError("StartTime", "time is invalid");
            }

            if (proposal.EndTime < DateTime.Now)
            {
                ModelState.AddModelError("EndTime", "back to the future");
            }

            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                proposal.AspNetUserId = currentUserID;
                
                /*if(proposal.EndTime < DateTime.Now)
                    return NotFound();*/


                _context.Add(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", proposal.AspNetUserId);
            return View(proposal);
        }

        // GET: Proposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (proposal.AspNetUserId == currentUserID) { 
                ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", proposal.AspNetUserId);
                return View(proposal);
            }
            else return NotFound();
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,StartTime,EndTime,Topic,Content,Point,MaxPeople,Status,AspNetUserId")] Proposal proposal)
        {
            if (id != proposal.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proposal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProposalExists(proposal.id))
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
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", proposal.AspNetUserId);
            return View(proposal);
        }


        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public async Task<IActionResult> Consider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _context.Proposals
                .Include(p => p.AspNetUser)
                .FirstOrDefaultAsync(m => m.id == id);

            if (proposal == null)
            {
                return NotFound();
            }

            return View(proposal);
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Consider(int id, [Bind("id, StartTime, EndTime, Topic, Content, Point, MaxPeople, Status, AspNetUserId")] Proposal proposal)
        {
            if (id != proposal.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proposal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProposalExists(proposal.id))
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
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", proposal.AspNetUserId);
            return View(proposal);
        }



        // GET: Proposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _context.Proposals
                .Include(p => p.AspNetUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (proposal == null)
            {
                return NotFound();
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (proposal.AspNetUserId == currentUserID)
                return View(proposal);
            else return Redirect("~/Error/ErrorNoPower");

        }

        // POST: Proposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Status()
        {
            //_context.Users.Where(f => f.Email =="qwe").First();
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var applicationDbContext = _context.Proposals.Include(p => p.AspNetUser);
            ViewBag.Fid = currentUserID;
            ViewData["Id"] = currentUserID;
            ViewData["name"] = User.Identity.Name;
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> SeeAll()
        {
            var applicationDbContext = _context.Proposals.Include(p => p.AspNetUser);
            return View(await applicationDbContext.ToListAsync());
        }

        private bool ProposalExists(int id)
        {
            return _context.Proposals.Any(e => e.id == id);
        }
    }
}
