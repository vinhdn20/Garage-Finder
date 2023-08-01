using AutoMapper;
using DataAccess.DTO.Report;
using GFData.Data;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public ReportService(IReportRepository reportRepository, IGarageRepository garageRepository, IMapper mapper, IUsersRepository usersRepository)
        {
            _reportRepository = reportRepository;
            _garageRepository = garageRepository;
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public void AddReport(AddReportDTO addReport, int userID)
        {
            var garage = _garageRepository.GetGaragesByID(addReport.GarageID);

            Report report = new Report()
            {
                Date = DateTime.UtcNow,
                Reason = addReport.Reason,
                UserID = userID,
                GarageID = garage.GarageID
            };
            _reportRepository.Add(report);
        }

        public List<ReportDTO> GetList()
        {
            List<ReportDTO> reportDTO = new List<ReportDTO>();
            var report = _reportRepository.GetList();
            foreach (var rp in report)
            {
                var user = _usersRepository.GetUserByID(rp.UserID);
                var garage = _garageRepository.GetGaragesByID(rp.GarageID);
                var rpDTO = _mapper.Map<ReportDTO>(rp);
                rpDTO.GarageName = garage.GarageName;
                rpDTO.GaragePhone = garage.PhoneNumber;
                rpDTO.GarageMail = garage.EmailAddress;
                rpDTO.UserEmail = user.EmailAddress;
                reportDTO.Add(rpDTO);

            }
            return reportDTO;
        }
    }
}

