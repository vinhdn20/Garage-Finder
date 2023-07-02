using DataAccess.DAO;
using DataAccess.DTO.Staff;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.StaffRepository
{
    public class StaffRepository : IStaffRepository
    {
        public Staff AddStaff(Staff staff)
        {
            return StaffDAO.Instance.AddStaff(staff);
        }

        public List<Staff> GetByGarageId(int id)
        {
            return StaffDAO.Instance.GetStaffByGarageID(id);
        }

        public void UpdateStaff(Staff staff)
        {
            StaffDAO.Instance.UpdateStaff(staff);
        }

        public void DeleteStaff(int id)
        {
            StaffDAO.Instance.DeleteStaff(id);
        }

        public Staff GetById(int id)
        {
            return StaffDAO.Instance.GetStaffById(id);
        }
    }
}
