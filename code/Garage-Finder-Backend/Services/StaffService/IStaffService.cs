using DataAccess.DTO.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StaffService
{
    public interface IStaffService
    {
        List<StaffDTO> GetByGarageID(int id, int userId);
        StaffDTO AddStaff(AddStaffDTO staff, int userId);
        void UpdateStaff(UpdateStaffDTO staff, int userId);
        void DeleteStaff(int id, int userId);
        void UpdateStatus(BlockStaffDTO blockStaff, int userId);
    }
}
