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
            return View(await _context.actualExpences.ToListAsync());
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
        public IActionResult Create()
        {
            
            var expenceId = TempData["expenceId"];
            var userId = TempData["userId"];
            var name = TempData["name"];

            ViewBag.expenceId = expenceId;
            ViewBag.userId = userId;
            ViewBag.name = name;

            return View();
        }

        // POST: actualExpences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,finalCost,userId,expenceId,category,datePayed")] actualExpence actualExpence)
        {
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
