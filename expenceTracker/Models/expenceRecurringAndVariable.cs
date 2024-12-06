using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace expenceTracker.Models
{
    public class expenceRecurringAndVariable
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public double cost { get; set; }
        [AllowNull]
        public string? frequency { get; set; }

        public int userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }

    }
}
