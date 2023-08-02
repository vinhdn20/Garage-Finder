using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Report
{
    public class AddReportDTO
    {
        public int ReportID { get; set; }
        public int GarageID { get; set; }
        public string Reason { get; set; }
        public List<string>? ImageLink { get; set; }
    }
}
