using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Services.RequestSerivesDTO
{
    public class AddServiceDTO
    {
        public string NameService { get; set; }
        public int CategoryGarageID { get; set; }
        public double? Cost { get; set; }
        public string? Note { get; set; }
    }
}
