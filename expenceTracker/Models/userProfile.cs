using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenceTracker.Models
{
    public class userProfile
    {
        [Key]
        public int Id { get; set; }
        public bool Darkmode { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
        
    }
}
