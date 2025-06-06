using Microsoft.AspNetCore.Mvc;
using RKIT.Models;
using System.Diagnostics;

namespace RKIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalIncome = _context.Incomes.Sum(i => i.Amount);
            var totalExpense = _context.Expenses.Sum(e => e.Amount);
            var balance = totalIncome - totalExpense;

            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpense = totalExpense;
            ViewBag.Balance = balance;

            return View();
        }
    }
}
