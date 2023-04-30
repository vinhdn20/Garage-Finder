namespace Garage_Finder_Backend.Models.Entity
{
    public class Service
    {
        public int ServiceID { get; set; }
        public int GarageID { get; set; }
        public string NameService { get; set; }
        public float Cost { get; set; }
        public string Note { get; set; }
    }
}
