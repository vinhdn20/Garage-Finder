namespace Garage_Finder_Backend.Models.Entity
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ServiceID { get; set; }
        public string NameService { get; set; }
        public float Cost { get; set; }
        public string Note { get; set; }

    }
}
