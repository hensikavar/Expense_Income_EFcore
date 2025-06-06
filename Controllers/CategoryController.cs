using Microsoft.AspNetCore.Mvc;
using RKIT.Models;

namespace RKIT.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new CategoryModel()); // Create mode
            }
            else
            {
                var category = _context.Categories.Find(id);
                if (category == null)
                    return NotFound();

                return View(category); // Edit mode
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    var key = state.Key;
                    var errors = state.Value.Errors;
                    foreach (var error in errors)
                    {
                       Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                  }
                }
            }
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _context.Categories.Add(category); // Create
                }
                else
                {
                    _context.Categories.Update(category); // Edit
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
