using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestDTO.Car
{
    public class UpdateCarDTO
    {
        public int CarID { get; set; }
        public string LicensePlates { get; set; }
        public int BrandID { get; set; }
        public string Color { get; set; }
        public string TypeCar { get; set; }
        public string Avatar { get; set; }
    }
}
