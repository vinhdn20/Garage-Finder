using AutoMapper;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Staff;
using GFData.Models.Entity;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IMapper _mapper;

        public StaffService(IStaffRepository staffRepository, IGarageRepository garageRepository,
            IMapper mapper)
        {
            _staffRepository = staffRepository;
            _garageRepository = garageRepository;
            _mapper = mapper;
        }
        public StaffDTO AddStaff(AddStaffDTO addStaff, int userId)
        {
            if(!ValidationGarageOwner(addStaff.GarageID, userId))
            {
                throw new Exception("Authorize exception!");
            }

            Staff staff = _mapper.Map<Staff>(addStaff);
            var staffAdd = _staffRepository.AddStaff(staff);
            return _mapper.Map<StaffDTO>(staffAdd);
        }

        public void DeleteStaff(int id, int userId)
        {
            var staff = _staffRepository.GetById(id);
            if (!ValidationGarageOwner(staff.GarageID, userId))
            {
                throw new Exception("Authorize exception!");
            }
            _staffRepository.DeleteStaff(id);
        }

        public List<StaffDTO> GetByGarageID(int id, int userId)
        {
            if (!ValidationGarageOwner(id, userId))
            {
                throw new Exception("Authorize exception!");
            }
            var staffList = _staffRepository.GetByGarageId(id);
            var results = staffList.Select(x => _mapper.Map<StaffDTO>(x)).ToList();
            return results;
        }

        public void UpdateStaff(UpdateStaffDTO staff, int userId)
        {
            var stafDB = _staffRepository.GetById(staff.StaffId);
            if (!ValidationGarageOwner(stafDB.GarageID, userId) || userId == staff.StaffId)
            {
                throw new Exception("Authorize exception!");
            }
            var staffUpdate = _mapper.Map<Staff>(staff);
            _staffRepository.UpdateStaff(staffUpdate);
        }

        public void UpdateStatus(BlockStaffDTO blockStaff, int userId)
        {
            var stafDB = _staffRepository.GetById(blockStaff.StaffID);
            if (!ValidationGarageOwner(stafDB.GarageID, userId))
            {
                throw new Exception("Authorize exception!");
            }

            if(!Constants.STAFF_STATUS.Contains(blockStaff.Status))
            {
                throw new Exception("Status invalid");
            }
            var staff = _staffRepository.GetById(blockStaff.StaffID);
            staff.Status = blockStaff.Status;
            _staffRepository.UpdateStaff(staff);
        }

        private bool ValidationGarageOwner(int garageId, int userId)
        {
            var garages = _garageRepository.GetGarageByUser(userId);
            if (!garages.Any(x => x.GarageID == garageId))
            {
                return false;
            }
            return true;
        }
    }
}
