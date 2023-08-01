using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Garage;
using DataAccess.DTO.Report;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.ReportRepository
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMapper _mapper;
        public ReportRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<Report> GetList()
        {
            return ReportDAO.Instance.GetList().ToList();
        }


        public void Add(Report report)
        {
            ReportDAO.Instance.SaveList(report);
        }
    }
}
