using System.ComponentModel.DataAnnotations;

namespace Garage_Finder_Backend.Models.RequestModels
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
