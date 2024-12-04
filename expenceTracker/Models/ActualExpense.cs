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
        public double? FinalCost { get; set; }

        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        UserProfile UserProfile { get; set; }

        public int ExpenseId { get; set; }

        [ForeignKey("ExpenseId")]
        ExpenseMonth ExpenseMonth { get; set; }

        [AllowNull]
        public string? Category { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "invalid Date")]
        public DateOnly DatePaid { get; set; }

    }
}
