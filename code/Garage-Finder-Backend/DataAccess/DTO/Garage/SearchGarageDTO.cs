using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Garage
{
    public class SearchGarageDTO
    {
        public List<GarageDTO> garages { get; set; }
        public double Total { get; set; }
    }
}
