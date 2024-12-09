using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenceTracker.Models
{
    public class expectedExpences
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }

        public int expenceId { get; set; }

        [ForeignKey("expenceId")]
        public monthlyExpence monthlyExpence { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        public double predictedCost { get; set; }

        public string type { get; set; }

        public string? frequency { get; set; } 
    }
}