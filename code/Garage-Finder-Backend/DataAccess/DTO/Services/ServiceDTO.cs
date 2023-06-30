using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO.Services
{
    public class ServiceDTO
    {
        public int ServiceID { get; set; }
        public string NameService { get; set; }
        public int CategoryGarageID { get; set; }
        public double? Cost { get; set; }
        public string? Note { get; set; }
        //public virtual CategoryDTO? Category { get; set; }
    }
}
