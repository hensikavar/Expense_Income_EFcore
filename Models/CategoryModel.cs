using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RKIT.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("Income|Expense")]
        public string Type { get; set; }  // Either "Income" or "Expense"

        // Navigation properties
        [ValidateNever]
        public ICollection<IncomeModel> Incomes { get; set; }

        [ValidateNever]
        public ICollection<ExpenseModel> Expenses { get; set; }
    }
}
