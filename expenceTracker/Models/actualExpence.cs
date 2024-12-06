using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace expenceTracker.Models
{
    public class actualExpence //Payment table
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public double? finalCost { get; set; }

        public int userId { get; set; }

        [ForeignKey("userId")]
        User User { get; set; }

        public int expenceId { get; set; }

        [ForeignKey("expenceId")]
        expectedExpences expenceMonth { get; set; }

        [AllowNull]
        public string? category { get; set; }
        [Required]
        [DataType(DataType.Date,ErrorMessage ="invalid Date")]
        public DateOnly datePayed { get; set; }




    }
}
