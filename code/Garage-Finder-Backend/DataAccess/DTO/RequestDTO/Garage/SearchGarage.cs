using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestDTO.Garage
{
    public class SearchGarage
    {
        public string? keyword { get; set; }
        public int? provinceID { get; set; }
        public int? districtsID { get; set; }
        public int? categoriesID { get; set; }
    }
}
