using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class ExpenseMonth
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public required double PredictedCost { get; set; }


        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public UserProfile UserProfile { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]

        public required DateOnly DateDue { get; set; }

    }
}
