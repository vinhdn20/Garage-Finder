using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ReportDAO
    {
        private static ReportDAO instance = null;
        private static readonly object iLock = new object();
        public ReportDAO()
        {

        }
        public static ReportDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Report> GetList()
        {
            var reportList = new List<Report>();
            try
            {
                using (var context = new GFDbContext())
                {
                    reportList = (from report in context.Report
                                  select new Report
                                  {
                                      ReportID = report.ReportID,
                                      Reason = report.Reason,
                                      Date = report.Date,
                                      GarageID = report.GarageID,
                                      UserID = report.UserID,
                                      ImageReport = (from imageReport in context.ImageReport
                                                     where imageReport.ReportID == report.ReportID
                                                     select imageReport).ToList(),
                                  }
                                    ).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return reportList;
        }

        public void SaveList(Report reportList)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Report.Add(reportList);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateReport(Report reportList)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Report.Update(reportList);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
