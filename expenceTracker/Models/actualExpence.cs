using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace expenceTracker.Models
{
    public class actualExpence
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public double? finalCost { get; set; }

        public int profileId { get; set; }

        [ForeignKey("profileId")]
        userProfile userProfile { get; set; }

        public int expenceID { get; set; }

        [ForeignKey("expenceId")]
        expenceMonth expenceMonth { get; set; }

        [AllowNull]
        public string? category { get; set; }
        [Required]
        [DataType(DataType.Date,ErrorMessage ="invalid Date")]
        public DateOnly datePayed { get; set; }




    }
}
