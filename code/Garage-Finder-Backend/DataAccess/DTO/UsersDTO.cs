﻿using GFData.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DTO
{
    public class UsersDTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string LinkImage { get; set; }
        public int RoleID { get; set; }
        public RoleNameDTO roleName { get; set; }
        
        public string AccessToken { get; set; }
    }
}
