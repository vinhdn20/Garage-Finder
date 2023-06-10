namespace DataAccess.DTO
{
    public class CarDTO
    {
        public int CarID { get; set; }
        public int UserID { get; set; } 
        public string LicensePlates { get; set; }
        public int BrandID { get; set; }   
        public string Color { get; set; }   
        public string TypeCar { get; set; }
        public List<string> ImageCar { get; set; }
    }
}
