using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public bool Darkmode { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

    }
}
