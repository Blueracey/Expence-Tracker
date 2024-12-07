using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenceTracker.Models
{
    public class monthlyExpence
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateOnly date { get; set; }

        public int userId   { get; set; }

        [ForeignKey("userId")]

         User User { get; set; }


        [Required(ErrorMessage = "Budget is required")]
        public double budget {  get; set; }

    }
}
