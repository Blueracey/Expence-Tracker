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
            return View(await _context.expectedExpence.ToListAsync());
        }

        // GET: expenceMonths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expectedExpence = await _context.expectedExpence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expectedExpence == null)
            {
                return NotFound();
            }

            return View(expectedExpence);
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
        public async Task<IActionResult> Create([Bind("Id,name,predictedCost,profileId,dateDue")] expectedExpences expectedExpence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expectedExpence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expectedExpence);
        }

        // GET: expenceMonths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expectedExpence = await _context.expectedExpence.FindAsync(id);
            if (expectedExpence == null)
            {
                return NotFound();
            }
            return View(expectedExpence);
        }

        // POST: expenceMonths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,predictedCost,profileId,dateDue")] expectedExpences expectedExpence)
        {
            if (id != expectedExpence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expectedExpence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!expenceMonthExists(expectedExpence.Id))
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
            return View(expectedExpence);
        }

        // GET: expenceMonths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenceMonth = await _context.expectedExpence
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
            var expectedExpence = await _context.expectedExpence.FindAsync(id);
            if (expectedExpence != null)
            {
                _context.expectedExpence.Remove(expectedExpence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool expenceMonthExists(int id)
        {
            return _context.expectedExpence.Any(e => e.Id == id);
        }




        // cotnrolelrs below this line are for the actual expences side of things



        // GET: actualExpences/Create
        [HttpGet("ResolveExpence/{id}/{profileId}/{predictedCost}")]
        public IActionResult ResolveExpence(int id, int profileId, double predictedCost ) // first int is from expenceId second is from 
        {

            ViewBag.profileId = profileId;
            ViewBag.expenceId = id;
            ViewBag.finalCost = predictedCost;
            return View();
        }


        [HttpPost("ResolveExpence/{id}/{profileId}/{predictedCost}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResolveExpence([Bind("finalCost,profileId,expenceID,category,datePayed")] actualExpence actualExpence)
        {


            if (ModelState.IsValid)
            {
                _context.Add(actualExpence);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(actualExpence);
        }

        // data call for resolved expences
        public async Task<IActionResult> expenceMonthView()
        {
            return View(await _context.actualExpences.ToListAsync());
        }

        // GET: actualExpences/Delete/5
        public async Task<IActionResult> expenceFinalDelete(int? id)
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
        [HttpPost, ActionName("DeleteExpenceHistory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpenceHistory(int id)
        {
            var actualExpence = await _context.actualExpences.FindAsync(id);
            if (actualExpence != null)
            {
                _context.actualExpences.Remove(actualExpence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // details display controller
        public async Task<IActionResult> FinalDetails(int? id)
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



        // visual for edit 
        public async Task<IActionResult> FinalEdit(int? id)
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

        //actual action for edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalEditUpdate(int id, [Bind("Id,finalCost,profileId,expenceID,category,datePayed")] actualExpence actualExpence)
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
                return RedirectToAction(nameof(actualExpence));
            }
            return View(actualExpence);
        }



        private bool actualExpenceExists(int id)
        {
            return _context.actualExpences.Any(e => e.Id == id);
        }



    }










}
