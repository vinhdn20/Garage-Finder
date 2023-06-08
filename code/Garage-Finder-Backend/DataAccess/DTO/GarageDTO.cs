using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public int UserID { get; set; }
        public string GarageName { get; set; }
        public string AddressDetail { get; set; }
        public string ProvinceID { get; set; }
        public string DistrictsID { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public string? Logo { get; set; }
        public string? Imagies { get; set; }
        public double? LatAddress { get; set; }
        public double? LngAddress { get; set; }
        public ServiceDTO ServiceDTO { get; set; }
        public GarageBrandDTO GarageBrandDTO { get; set; }
    }
}
