using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using expenceTracker.Data;
using expenceTracker.Models;

//https://localhost:7267/expenceMonths

namespace expenceTracker.Controllers
{
    public class expenceMonthsController : Controller
    {
        private readonly AppDatabaseContext _context;

        public expenceMonthsController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: expenceMonths
        public async Task<IActionResult> Index()
        {
            return View(await _context.expenceMonths.ToListAsync());
        }

        // GET: expenceMonths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenceMonth = await _context.expenceMonths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenceMonth == null)
            {
                return NotFound();
            }

            return View(expenceMonth);
        }

        // GET: expenceMonths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: expenceMonths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,predictedCost,profileId,dateDue")] expenceMonth expenceMonth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenceMonth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenceMonth);
        }

        // GET: expenceMonths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenceMonth = await _context.expenceMonths.FindAsync(id);
            if (expenceMonth == null)
            {
                return NotFound();
            }
            return View(expenceMonth);
        }

        // POST: expenceMonths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,predictedCost,profileId,dateDue")] expenceMonth expenceMonth)
        {
            if (id != expenceMonth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenceMonth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!expenceMonthExists(expenceMonth.Id))
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
            return View(expenceMonth);
        }

        // GET: expenceMonths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenceMonth = await _context.expenceMonths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenceMonth == null)
            {
                return NotFound();
            }

            return View(expenceMonth);
        }

        // POST: expenceMonths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenceMonth = await _context.expenceMonths.FindAsync(id);
            if (expenceMonth != null)
            {
                _context.expenceMonths.Remove(expenceMonth);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool expenceMonthExists(int id)
        {
            return _context.expenceMonths.Any(e => e.Id == id);
        }
    }
}
