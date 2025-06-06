using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RKIT.Models;

namespace RKIT.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;
        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Expense/Index
        public IActionResult Index()
        {
            // Include Category so that you can display Category.Name in the table
            var expenses = _context.Expenses
                                   .Include(e => e.Category)
                                   .ToList();
            return View(expenses);
        }

        // GET: Expense/Create/{id?}
        // If id is null or 0 → Create mode. Otherwise → load for Edit.
        public IActionResult Create(int? id)
        {
            // Prepare a SelectList of all “Expense”-type categories
            var categories = _context.Categories
                                     .Where(c => c.Type == "Expense")
                                     .ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

            if (id == null || id == 0)
            {
                // Create mode: pass a fresh ExpenseModel
                return View(new ExpenseModel());
            }
            else
            {
                // Edit mode: load existing expense
                var expense = _context.Expenses
                                      .Include(e => e.Category)
                                      .FirstOrDefault(e => e.ExpenseId == id.Value);

                if (expense == null)
                    return NotFound();

                return View(expense);
            }
        }

        // POST: Expense/Create   (this handles both Create and Edit)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                if (expense.ExpenseId == 0)
                {
                    // New record
                    _context.Expenses.Add(expense);
                }
                else
                {
                    // Existing record → update
                    _context.Expenses.Update(expense);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If we got here, something was invalid → re‐populate the Category dropdown
            var categories = _context.Categories
                                     .Where(c => c.Type == "Expense")
                                     .ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

            return View(expense);
        }

        // GET: Expense/Delete/{id}
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
                return NotFound();

            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
