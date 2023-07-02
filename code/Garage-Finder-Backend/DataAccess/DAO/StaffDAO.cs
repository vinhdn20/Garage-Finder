using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class StaffDAO
    {
        private static StaffDAO instance = null;
        private static readonly object iLock = new object();
        public StaffDAO()
        {

        }

        public static StaffDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new StaffDAO();
                    }
                    return instance;
                }

            }
        }

        public List<Staff> GetStaffByGarageID(int GarageID)
        {
            List<Staff> staffList = new List<Staff>();
            try
            {
                using (var context = new GFDbContext())
                {
                    staffList = context.Staff.Where(x => x.GarageID == GarageID).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return staffList;
        }

        public Staff GetStaffById(int id)
        {
            Staff staff = new Staff();
            try
            {
                using (var context = new GFDbContext())
                {
                    staff = context.Staff.Where(x => x.StaffId == id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return staff;
        }

        public void UpdateStaff(Staff staff)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Staff.Update(staff);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Staff AddStaff(Staff staff)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Staff.Add(staff);
                    context.SaveChanges();
                    return staff;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteStaff(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var staff = context.Staff.Where(x => x.StaffId == id).FirstOrDefault();
                    context.Staff.Remove(staff);
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
