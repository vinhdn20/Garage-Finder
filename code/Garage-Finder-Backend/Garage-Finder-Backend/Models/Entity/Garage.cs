namespace Garage_Finder_Backend.Models.Entity
{
    public class Garage
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
