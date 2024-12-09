


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using expenceTracker.Data;
using expenceTracker.Models;

namespace expenceTracker.Controllers
{
    public class actualExpencesController : Controller
    {
        private readonly AppDatabaseContext _context;

        public actualExpencesController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: actualExpences
        public async Task<IActionResult> Index()
        {
            var expenceId = Convert.ToInt32(TempData["expenceId"]);
            var userId = Convert.ToInt32(TempData["userId"]);
            var filtered = await _context.actualExpences
            .Where(e => e.expenceId == expenceId && e.userId == userId)  // Filtering based on Category
            .ToListAsync();
            return View(filtered);


        }

        // GET: actualExpences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
    }
}
