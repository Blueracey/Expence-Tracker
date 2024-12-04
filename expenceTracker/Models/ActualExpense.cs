using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Models
{
    public class ActualExpense
    {
        [Key]
        public int Id { get; set; }

        [AllowNull]
        public double? finalCost { get; set; }

        public int profileId { get; set; }

        [ForeignKey("profileId")]
        UserProfile userProfile { get; set; }

        public int expenseId { get; set; }

        [ForeignKey("expenseId")]
        ExpenseMonth expenseMonth { get; set; }

        [AllowNull]
        public string? category { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        public DateOnly datePaid { get; set; }

    }
}
