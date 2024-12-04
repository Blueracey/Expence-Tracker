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
        public string name { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        public double cost { get; set; }

        [AllowNull]
        public string? frequency { get; set; }

        [ForeignKey("profileId")]
        UserProfile userProfile { get; set; }

    }
}
