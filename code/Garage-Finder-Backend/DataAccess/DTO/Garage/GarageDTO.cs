using GFData.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO.Garage
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public string GarageName { get; set; }
        public string AddressDetail { get; set; }
        public int? ProvinceID { get; set; }
        public int? DistrictsID { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? OpenTime { get; set; }
        public string? CloseTime { get; set; }
        public string? Thumbnail { get; set; }
        public double? LatAddress { get; set; }
        public double? LngAddress { get; set; }
        public int UserID { get; set; }
        public double Star { get; set; }
        public int FeedbacksNumber { get; set; }
        public List<CategoryGarageDTO>? CategoryGarages { get; set; }
        public List<GarageBrandDTO>? GarageBrands { get; set; }
        public List<ImageGarageDTO>? ImageGarages { get; set; }
    }
}
