using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenceTracker.Models
{
    public class expectedExpences
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public double predictedCost { get; set; }


        public int userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }


        public int expenceID { get; set; }

        [ForeignKey("expenceId")]
        expectedExpences expenceMonth { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]

        public DateOnly dateDue { get; set; }


    }
}
