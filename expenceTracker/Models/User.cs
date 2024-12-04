using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
    }
}
