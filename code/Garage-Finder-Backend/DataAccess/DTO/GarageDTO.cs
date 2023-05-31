using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public int UserID { get; set; }
        public string GarageName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string OpenTime { get; set; }
        public string Logo { get; set; }
        public string Imagies { get; set; }
        public string Location { get; set; }
    }
}
