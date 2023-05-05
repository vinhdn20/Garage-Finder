using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public int UserID { get; set; }
        public string GarageName { get; set; }
        public string Address { get; set; }
        public string OwnerName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
