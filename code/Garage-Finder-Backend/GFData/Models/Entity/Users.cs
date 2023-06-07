using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    [Index(nameof(PhoneNumber), nameof(EmailAddress), IsUnique = true)]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Status { get; set; }
        public string? LinkImage { get; set; }
        
        [ForeignKey("RoleName")]
        public int RoleID { get; set; }
        
        public  RoleName RoleName { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Garage> Garages { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<FavoriteList> FavoriteList { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Invoices> Invoices { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public ICollection<GarageInfo> GarageInfos { get; set; }
    }
}
