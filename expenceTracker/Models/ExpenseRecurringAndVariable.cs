using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Models
{
    public class ExpenseRecurringAndVariable
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public double Cost { get; set; }
        [AllowNull]
        public string? Frequency { get; set; }

        [ForeignKey("ProfileId")]
        UserProfile UserProfile { get; set; }

    }
}
