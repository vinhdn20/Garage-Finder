using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class ServiceDTO
    {
        public int ServiceID { get; set; }
        public int GarageID { get; set; }
        public string NameService { get; set; }
        public int CategoryID { get; set; }
        public double? Cost { get; set; }
        public string? Note { get; set; }
        //public virtual CategoryDTO? Category { get; set; }
    }
}
