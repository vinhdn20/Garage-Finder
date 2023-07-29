using System.Runtime.Serialization;

namespace DataAccess.DTO.Garage
{
    public class AddGarageDTO
    {
        public string GarageName { get; set; }
        public string AddressDetail { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictsID { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OpenTime { get; set; }
        public string? CloseTime { get; set; }
        public string? Thumbnail { get; set; }
        public string? Imagies { get; set; }
        public double? LatAddress { get; set; }
        public double? LngAddress { get; set; }
        //public List<CategoryDTO> Categories { get; set; }
        public List<int> CategoriesID { get; set; }
        //public List<BrandDTO> Brands { get; set; }
        public List<int> BrandsID { get; set; }
        //public List<ImageGarageDTO> ImageGarages { get; set; }
        public List<string> ImageLink { get; set; }
        public string? Status { get; set; }
    }
}
