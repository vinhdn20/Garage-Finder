using DataAccess.DTO.Report;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReportRepository
    {
        public List<Report> GetList();
        public void Add(Report report);
        public void Update(Report report);
        public ReportDTO GetReportByID(int id);
    }
}
