namespace Garage_Finder_Backend.Models.Entity
{
    public class Car
    {
        public int CarID { get; set; }
        public int UserID { get; set; } 
        public string LicensePlates { get; set; }
        public string Brand { get; set; }   
        public string Color { get; set; }   
        public string Type { get; set; }

    }
}
