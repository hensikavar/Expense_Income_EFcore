using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RKIT.Models
{
    public class ExpenseModel
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        // Foreign key
        public int? CategoryId { get; set; }

        // Navigation property
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public CategoryModel Category { get; set; }
    }
}
