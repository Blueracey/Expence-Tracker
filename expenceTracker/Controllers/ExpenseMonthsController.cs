using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

//https://localhost:7267/ExpenseMonths

namespace ExpenseTracker.Controllers
{
    public class ExpenseMonthsController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseMonthsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExpenseMonths
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseMonths.ToListAsync());
        }

        // GET: ExpenseMonths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ExpenseMonth = await _context.ExpenseMonths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ExpenseMonth == null)
            {
                return NotFound();
            }

            return View(ExpenseMonth);
        }

        // GET: ExpenseMonths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseMonths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,predictedCost,profileId,dateDue")] ExpenseMonth ExpenseMonth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ExpenseMonth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ExpenseMonth);
        }

        // GET: ExpenseMonths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ExpenseMonth = await _context.ExpenseMonths.FindAsync(id);
            if (ExpenseMonth == null)
            {
                return NotFound();
            }
            return View(ExpenseMonth);
        }

        // POST: ExpenseMonths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,predictedCost,profileId,dateDue")] ExpenseMonth ExpenseMonth)
        {
            if (id != ExpenseMonth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ExpenseMonth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseMonthExists(ExpenseMonth.Id))
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
            return View(ExpenseMonth);
        }

        // GET: ExpenseMonths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ExpenseMonth = await _context.ExpenseMonths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ExpenseMonth == null)
            {
                return NotFound();
            }

            return View(ExpenseMonth);
        }

        // POST: ExpenseMonths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ExpenseMonth = await _context.ExpenseMonths.FindAsync(id);
            if (ExpenseMonth != null)
            {
                _context.ExpenseMonths.Remove(ExpenseMonth);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseMonthExists(int id)
        {
            return _context.ExpenseMonths.Any(e => e.Id == id);
        }




        // cotnrolelrs below this line are for the actual Expenses side of things



        // GET: ActualExpenses/Create
        [HttpGet("ResolveExpense/{id}/{profileId}/{predictedCost}")]
        public IActionResult ResolveExpense(int id, int profileId, double predictedCost) // first int is from ExpenseId second is from 
        {

            ViewBag.profileId = profileId;
            ViewBag.ExpenseId = id;
            ViewBag.finalCost = predictedCost;
            return View();
        }


        [HttpPost("ResolveExpense/{id}/{profileId}/{predictedCost}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResolveExpense([Bind("finalCost,profileId,ExpenseID,category,datePayed")] ActualExpense ActualExpense)
        {


            if (ModelState.IsValid)
            {
                _context.Add(ActualExpense);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(ActualExpense);
        }

        // data call for resolved Expenses
        public async Task<IActionResult> ExpenseMonthView()
        {
            return View(await _context.ActualExpenses.ToListAsync());
        }

        // GET: ActualExpenses/Delete/5
        public async Task<IActionResult> ExpenseFinalDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ActualExpense = await _context.ActualExpenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ActualExpense == null)
            {
                return NotFound();
            }

            return View(ActualExpense);
        }


        // POST: ActualExpenses/Delete/5
        [HttpPost, ActionName("DeleteExpenseHistory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpenseHistory(int id)
        {
            var ActualExpense = await _context.ActualExpenses.FindAsync(id);
            if (ActualExpense != null)
            {
                _context.ActualExpenses.Remove(ActualExpense);
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

            var ActualExpense = await _context.ActualExpenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ActualExpense == null)
            {
                return NotFound();
            }

            return View(ActualExpense);
        }



        // visual for edit 
        public async Task<IActionResult> FinalEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ActualExpense = await _context.ActualExpenses.FindAsync(id);
            if (ActualExpense == null)
            {
                return NotFound();
            }
            return View(ActualExpense);
        }

        //actual action for edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalEditUpdate(int id, [Bind("Id,finalCost,profileId,ExpenseID,category,datePayed")] ActualExpense ActualExpense)
        {
            if (id != ActualExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ActualExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActualExpenseExists(ActualExpense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ActualExpense));
            }
            return View(ActualExpense);
        }

        private bool ActualExpenseExists(int id)
        {
            return _context.ActualExpenses.Any(e => e.Id == id);
        }

    }

}
