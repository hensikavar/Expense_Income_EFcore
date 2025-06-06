using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RKIT.Models;

namespace RKIT.Controllers
{
    public class IncomeController : Controller
    {
        private readonly AppDbContext _context;
        public IncomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var incomes = _context.Incomes.Include(i => i.Category).ToList();
            return View(incomes);
        }



        public IActionResult Create(int? id)
        
        {
            var categories = _context.Categories.Where(c => c.Type == "Income").ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");


            if (id == null || id == 0)
            {
                return View(new IncomeModel()); // Create mode
            }
            else
            {
                var income = _context.Incomes
                                     .Include(i => i.Category)
                                     .FirstOrDefault(i => i.IncomeId == id);
                if (income == null)
                    return NotFound();

                return View(income); // Edit mode
            }

        }

        // POST: Income/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IncomeModel income)
        {
            //Console.WriteLine(income.IncomeId);
            //if (!ModelState.IsValid)
            //{
            //    foreach (var state in ModelState)
            //    {
            //        var key = state.Key;
            //        var errors = state.Value.Errors;
            //        foreach (var error in errors)
            //        {
            //            Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
            //        }
            //    }
            //}
                if (ModelState.IsValid)
            {
                if (income.IncomeId == 0)
                {
                    _context.Incomes.Add(income); // Create
                }
                else
                {
                    _context.Incomes.Update(income); // Edit
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var categories = _context.Categories.Where(c => c.Type == "Income").ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");


            return View(income);
        }
        public IActionResult Delete(int id)
        {
            var income = _context.Incomes.Find(id);
            _context.Incomes.Remove(income);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
