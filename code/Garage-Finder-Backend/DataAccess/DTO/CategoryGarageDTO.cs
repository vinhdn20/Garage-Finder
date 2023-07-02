using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class CategoryGarageDTO
    {
        public int CategoryGarageID { get; set; }
        public int GarageID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
