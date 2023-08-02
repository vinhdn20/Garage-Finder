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
        private readonly IUsersRepository _usersRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IMapper _mapper;
        public ReportRepository(IMapper mapper, IUsersRepository usersRepository, IGarageRepository garageRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _garageRepository = garageRepository;
        }
        public List<Report> GetList()
        {
            return ReportDAO.Instance.GetList().ToList();
        }


        public void Add(Report report)
        {
            ReportDAO.Instance.SaveList(report);
        }

        public ReportDTO GetReportByID(int id)
        {
            var report = ReportDAO.Instance.GetList().Where(c => c.ReportID == id).Select(p => _mapper.Map<Report, ReportDTO>(p)).FirstOrDefault();
            var user = _usersRepository.GetUserByID(report.UserID);
            var garage = _garageRepository.GetGaragesByID(report.GarageID);
            report.GarageName = garage.GarageName;
            report.GaragePhone = garage.PhoneNumber;
            report.GarageMail = garage.EmailAddress;
            report.UserEmail = user.EmailAddress;
            return report;
        }

        public void Update(Report report) 
        {
            ReportDAO.Instance.UpdateReport(report);
        }
    }
}
