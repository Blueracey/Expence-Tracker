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
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace expenceTracker.Controllers
{
    [Authorize]
    public class expectedExpencesController : Controller
    {
        private readonly AppDatabaseContext _context;

        private readonly ILogger<expectedExpencesController> _logger;

        public expectedExpencesController(AppDatabaseContext context, ILogger<expectedExpencesController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: expectedExpences
        [HttpGet("expectedExpence/{expenceId}")]
        public async Task<IActionResult> Index(int expenceId)
        {
            string userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized();
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("Invalid user ID");
            }

            var filtered = await _context.expectedExpence
                .Where(e => e.expenceId == expenceId && e.userId == userId)
                .ToListAsync();

            ViewBag.userId = userId;
            ViewBag.expenceId = expenceId;

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
        public IActionResult Create(string expenceId)
        {
            ViewBag.userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            ViewBag.expenceId = expenceId ?? TempData["expenceId"]?.ToString();

            if (ViewBag.userId == null || ViewBag.expenceId == null)
            {
                return RedirectToAction("Index", "account");
            }

            return View();
        }

        // POST: expectedExpences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name, predictedCost, type, frequency, userId, expenceId")] expectedExpences expectedExpences)
        {
            string userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdString, out int userId);

            var user = await _context.Users.FindAsync(userId); 
            if (user == null)
            {
                _logger.LogError($"User with ID {userId} not found.");
                return View(expectedExpences);
            }

            expectedExpences.User = user;
            expectedExpences.userId = userId;

            var monthlyExpence = await _context.monthlyExpence.FindAsync(expectedExpences.expenceId);
            if (monthlyExpence == null)
            {
                _logger.LogError($"Monthly Expense with ID {expectedExpences.expenceId} not found.");
                return View(expectedExpences);
            }
            expectedExpences.monthlyExpence = monthlyExpence;

            if (expectedExpences.frequency != null && expectedExpences.predictedCost != 0)
            {
                double calculatedCost = expectedExpences.predictedCost;

                // Perform the calculation based on the frequency
                switch (expectedExpences.frequency)
                {
                    case "Weekly":
                        calculatedCost *= 4; // Multiply by 4 for weekly cost
                        break;
                    case "Biweekly":
                        calculatedCost *= 2; // Multiply by 2 for biweekly cost
                        break;
                    case "Monthly":
                        // No change for monthly as it's the base cost
                        break;
                    default:
                        break;
                }

                // Set the calculated cost
                expectedExpences.predictedCost = calculatedCost;
            }
            else
            {
                ModelState.AddModelError("predictedCost", "Predicted cost and frequency must be provided.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(expectedExpences);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { expenceId = expectedExpences.expenceId });
            } else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogInformation(error.ErrorMessage); 
                }
            }
            ViewBag.userId = userId;
            ViewBag.expenceId = expectedExpences.expenceId;
            return View(expectedExpences);
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
