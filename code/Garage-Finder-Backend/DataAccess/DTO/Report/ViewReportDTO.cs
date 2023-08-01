using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Report
{
    public class ViewReportDTO
    {
        public string ReportID { get; set; }
        public string Reason { get; set; }
        public List<string> ImageLink { get; set; }
        public DateTime Date { get; set; }
        public string GarageName { get; set; }
        public string GaragePhone { get; set; }
        public string GarageMail { get; set; }
        public string UserEmail { get; set; }
        public int GarageID { get; set; }
        public int UserID { get; set; }
    }
}

