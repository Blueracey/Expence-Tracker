


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using expenceTracker.Data;
using expenceTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace expenceTracker.Controllers
{
    [Authorize]
    public class actualExpencesController : Controller
    {
        private readonly AppDatabaseContext _context;

        public actualExpencesController(AppDatabaseContext context)
        {
            _context = context;
        }

        // converts the parameters to tempdata so that the index searches for the correct expencer/user id combo 
        [HttpGet("actualExpence/Redirect/{expenceId}/{userId}")]
        public IActionResult Redirect(int expenceId, int userId)
        {
            TempData["expenceId"] = expenceId;
            TempData["userId"] = userId;

            return RedirectToAction(nameof(Index));
        }

        // GET: actualExpences
        public async Task<IActionResult> Index()
        {
            var expenceId = Convert.ToInt32(TempData.Peek("expenceId"));
            var userId = Convert.ToInt32(TempData.Peek("userId"));
            TempData.Keep("userId");
            TempData.Keep("expenceId");
;            var filtered = await _context.actualExpences
            .Where(e => e.expenceId == expenceId && e.userId == userId)  // Filtering based on Category
            .ToListAsync();
            return View(filtered);


        }

        // GET: actualExpences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData.Keep("userId");
            TempData.Keep("expenceId");
            if (id == null)
            {
                return NotFound();
            }

            var actualExpence = await _context.actualExpences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualExpence == null)
            {
                return NotFound();
            }

            return View(actualExpence);
        }

        // GET: actualExpences/Create
        [HttpGet("actualExpence/{expenceId}/{userId}/{category}")]
        public IActionResult Create(int expenceId,int userId, string category)
        {
            System.Diagnostics.Debug.WriteLine("View");
            TempData["expenceId"] = expenceId;
            TempData["userId"] = userId;
            TempData["category"] = category;
            return View();
        }

        // POST: actualExpences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("actualExpence/{expenceId}/{userId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,finalCost,userId,expenceId,category,datePayed")] actualExpence actualExpence)
        {
            TempData.Keep("expenceId");
            TempData.Keep("userId");

            System.Diagnostics.Debug.WriteLine("running");
            if (ModelState.IsValid)
            {
                _context.Add(actualExpence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actualExpence);
        }

        // GET: actualExpences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData.Keep("userId");
            TempData.Keep("expenceId");
            if (id == null)
            {
                return NotFound();
            }

            var actualExpence = await _context.actualExpences.FindAsync(id);
            if (actualExpence == null)
            {
                return NotFound();
            }
            return View(actualExpence);
        }

        // POST: actualExpences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,finalCost,userId,expenceId,category,datePayed")] actualExpence actualExpence)
        {
            TempData.Keep("userId");
            TempData.Keep("expenceId");
            TempData.Keep("category");
            if (id != actualExpence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actualExpence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!actualExpenceExists(actualExpence.Id))
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
            return View(actualExpence);
        }

        // GET: actualExpences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData.Keep("userId");
            TempData.Keep("expenceId");
            if (id == null)
            {
                return NotFound();
            }

            var actualExpence = await _context.actualExpences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualExpence == null)
            {
                return NotFound();
            }

            return View(actualExpence);
        }

        // POST: actualExpences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData.Keep("userId");
            TempData.Keep("expenceId");
            var actualExpence = await _context.actualExpences.FindAsync(id);
            if (actualExpence != null)
            {
                _context.actualExpences.Remove(actualExpence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool actualExpenceExists(int id)
        {
            return _context.actualExpences.Any(e => e.Id == id);
        }

        // GET: actualExpences/Sum
        [HttpGet("actualExpence/sum")]
        public async Task<IActionResult> GetTotalActualExpenses(int expenceId)
        {
            string userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized();
            }

            int.TryParse(userIdString, out int userId);

            var sum = await _context.actualExpences
                .Where(e => e.expenceId == expenceId && e.userId == userId)
                .SumAsync(e => e.finalCost);

            return Ok(new { TotalActualExpenses = sum });
        }
    }
}
