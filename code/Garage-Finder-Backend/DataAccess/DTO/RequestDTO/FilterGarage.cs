using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestDTO
{
    public class FilterGarage
    {
        public List<int>? provinceID {get; set;}
        public List<int>? districtsID { get; set; }
        public List<int>? categoriesID { get; set; }
        public List<int>? brandsID { get; set; }
    }
}
