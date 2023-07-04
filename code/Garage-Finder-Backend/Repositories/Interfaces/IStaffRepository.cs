using DataAccess.DTO.Staff;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Staff GetById(int id);
        Staff AddStaff(Staff staff);
        void UpdateStaff(Staff staff);
        List<Staff> GetByGarageId(int id);
        void DeleteStaff(int id);
        Staff Login(string email, string password);
    }
}
