using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using expenceTracker.Data;
using expenceTracker.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace expenceTracker.Controllers
{
    public class expectedExpencesController : Controller
    {
        private readonly AppDatabaseContext _context;

        public expectedExpencesController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: expectedExpences
        [HttpGet("exectedExpence/{expenceId}/{userId}")]
        public async Task<IActionResult> Index(int expenceId, int userId)
        {
            TempData["expenceId"] = expenceId;
            TempData["userId"] = userId;
            

            var filtered = await _context.expectedExpence
            .Where(e => e.expenceId == expenceId && e.userId == userId)  // Filtering based on Category
            .ToListAsync();
            return View(filtered);

        }

        // GET: expectedExpences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData.Keep("expenceId");
            TempData.Keep("userId");

            if (id == null)
            {
                return NotFound();
            }

            var expectedExpences = await _context.expectedExpence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expectedExpences == null)
            {
                return NotFound();
            }

            TempData["expenceId"] = expectedExpences.expenceId;
            TempData["userId"] = expectedExpences.userId;
            TempData["name"] = expectedExpences.name;

            return View(expectedExpences);
        }

        // GET: expectedExpences/Create
        public IActionResult Create()
        {
            ViewBag.userId = TempData.Peek("userId");
            ViewBag.expenceId = TempData.Peek("expenceId");
            TempData.Keep("expenceId");
            TempData.Keep("userId");


            return View();
        }

        // POST: expectedExpences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,predictedCost,userId,expenceId,dateDue")] expectedExpences expectedExpences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expectedExpences);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "monthlyExpences");
            }
            return View();
        }

        // GET: expectedExpences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData.Keep("expenceId");
            TempData.Keep("userId");

            if (id == null)
            {
                return NotFound();
            }

            var expectedExpences = await _context.expectedExpence.FindAsync(id);
            if (expectedExpences == null)
            {
                return NotFound();
            }
            return View(expectedExpences);
        }

        // POST: expectedExpences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,predictedCost,userId,expenceId,dateDue")] expectedExpences expectedExpences)
        {
            TempData.Keep("expenceId");
            TempData.Keep("userId");

            if (id != expectedExpences.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expectedExpences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!expectedExpencesExists(expectedExpences.Id))
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
            return View(expectedExpences);
        }

        // GET: expectedExpences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData.Keep("expenceId");
            TempData.Keep("userId");

            if (id == null)
            {
                return NotFound();
            }

            var expectedExpences = await _context.expectedExpence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expectedExpences == null)
            {
                return NotFound();
            }

            return View(expectedExpences);
        }

        // POST: expectedExpences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            TempData.Keep("expenceId");
            TempData.Keep("userId");

            var expectedExpences = await _context.expectedExpence.FindAsync(id);
            if (expectedExpences != null)
            {
                _context.expectedExpence.Remove(expectedExpences);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool expectedExpencesExists(int id)
        {
            TempData.Keep("expenceId");
            TempData.Keep("userId");

            return _context.expectedExpence.Any(e => e.Id == id);
        }
    }
}
