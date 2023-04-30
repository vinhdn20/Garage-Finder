namespace Garage_Finder_Backend.Models.Entity
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public int GarageID { get; set; }
        public string TimeCreate { get; set; }
        public string TimeUpdate { get; set; }
        public string Status { get; set; }

    }
}
