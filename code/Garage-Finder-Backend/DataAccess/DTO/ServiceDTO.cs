using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class ServiceDTO
    {
        public int ServiceID { get; set; }
        public int GarageID { get; set; }
        public string NameService { get; set; }
        public float Cost { get; set; }
        public string Note { get; set; }
        public virtual CategoryDTO? Category { get; set; }
    }
}
