using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Garage
    {
        [Key]
        public int GarageID { get; set; }
        //[ForeignKey("User")]
        //public int UserID { get; set; }
        [Required]
        public string GarageName { get; set; }
        public string AddressDetail { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictsID { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? OpenTime { get; set; }
        public string? CloseTime { get; set; }
        public string? Thumbnail { get; set; }
        public double? LatAddress { get; set; }
        public double? LngAddress { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<FavoriteList> FavoriteList { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<GarageBrand> GarageBrands { get; set; }
        public ICollection<GarageInfo> GarageInfos { get; set; }
        public ICollection<CategoryGarage> CategoryGarages { get; set; }
        public ICollection<ImageGarage> ImageGarages { get; set; }
        //public Users User { get; set; }
    }
}
