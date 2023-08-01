using DataAccess.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ReportService
{
    public interface IReportService
    {
        public void AddReport(AddReportDTO addReport, int userID);
        public List<ReportDTO> GetList();
    }
}
