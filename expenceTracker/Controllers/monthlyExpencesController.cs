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
    public class monthlyExpencesController : Controller
    {
        private readonly AppDatabaseContext _context;

        public monthlyExpencesController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: monthlyExpences
        public async Task<IActionResult> Index()
        {
            return View(await _context.monthlyExpence.ToListAsync());
        }

        // GET: monthlyExpences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyExpence = await _context.monthlyExpence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monthlyExpence == null)
            {
                return NotFound();
            }

            return View(monthlyExpence);
        }

        // GET: monthlyExpences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: monthlyExpences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("date,userId,budget")] monthlyExpence monthlyExpence)
        {
            System.Diagnostics.Debug.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                _context.Add(monthlyExpence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monthlyExpence);
        }

        // GET: monthlyExpences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyExpence = await _context.monthlyExpence.FindAsync(id);
            if (monthlyExpence == null)
            {
                return NotFound();
            }
            return View(monthlyExpence);
        }

        // POST: monthlyExpences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,userId,budget")] monthlyExpence monthlyExpence)
        {
            if (id != monthlyExpence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monthlyExpence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!monthlyExpenceExists(monthlyExpence.Id))
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
            return View(monthlyExpence);
        }

        // GET: monthlyExpences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyExpence = await _context.monthlyExpence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monthlyExpence == null)
            {
                return NotFound();
            }

            return View(monthlyExpence);
        }

        // POST: monthlyExpences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monthlyExpence = await _context.monthlyExpence.FindAsync(id);
            if (monthlyExpence != null)
            {
                _context.monthlyExpence.Remove(monthlyExpence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool monthlyExpenceExists(int id)
        {
            return _context.monthlyExpence.Any(e => e.Id == id);
        }
    }
}
