using System.ComponentModel.DataAnnotations;

namespace expenceTracker.Models
{
    public class userRegisterDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
