using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }

        public RoleName RoleName { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Garage> Garages { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<FavoriteList> FavoriteList { get; set;}
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
