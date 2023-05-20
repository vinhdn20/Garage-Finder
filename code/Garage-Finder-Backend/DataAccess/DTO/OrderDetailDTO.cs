using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ServiceID { get; set; }
        public string NameService { get; set; }
        public float Cost { get; set; }
        public string Note { get; set; }
        public virtual ServiceDTO Service { get; set; }
        public int CategoryID { get; set; }

    }
}
