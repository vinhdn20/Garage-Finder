using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO.User.RequestDTO
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
