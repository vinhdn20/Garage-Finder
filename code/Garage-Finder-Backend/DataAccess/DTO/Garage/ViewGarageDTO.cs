using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Garage
{
    public class ViewGarageDTO
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
        public CategoryGarageDTO[]? CategoryGarages { get; set; }
        public List<ViewBrandDTO>? GarageBrands { get; set; } = new List<ViewBrandDTO>();
        public ImageGarageDTO[] ImageGarages { get; set; }
    }

    public class ViewBrandDTO
    {
        public int BrId { get; set; }
        public string BrandName { get; set; }
        public string LinkImage { get; set; }
    }
}
