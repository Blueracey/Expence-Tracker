using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenceTracker.Models
{
    public class expenceMonth
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public double predictedCost { get; set; }


        public double profileId {  get; set; }
        [ForeignKey("profileId")]
        userProfile userProfile { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]

        public DateOnly dateDue { get; set; }


    }
}
