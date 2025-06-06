using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RKIT.Models
{
    public class IncomeModel
    {
        [Key]
        public int IncomeId { get; set; }

        [Required]
        //[Range(0.0, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }

        // Foreign key
        public int? CategoryId { get; set; }

        // Navigation property
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public CategoryModel Category { get; set; }

    }
}
