﻿using Microsoft.EntityFrameworkCore;
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
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public string? AddressDetail { get; set; }
        public string? LinkImage { get; set; }
        
        [ForeignKey("RoleName")]
        public int RoleID { get; set; }
        
        public  RoleName RoleName { get; set; }

        public ICollection<Car> Cars { get; set; }
        //public ICollection<Garage> Garages { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<FavoriteList> FavoriteList { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<RoomChat> RoomChat { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
