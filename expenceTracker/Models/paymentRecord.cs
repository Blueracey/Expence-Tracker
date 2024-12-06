using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace expenceTracker.Models
{
    public class paymentRecord
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public double? finalCost { get; set; }

        public int userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }


        [AllowNull]
        public string? category { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "invalid Date")]
        public DateOnly datePayed { get; set; }
    }
}
