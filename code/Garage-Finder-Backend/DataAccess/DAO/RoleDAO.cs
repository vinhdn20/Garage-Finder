using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object iLock = new object();
        public RoleDAO()
        {

        }

        public static RoleDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }

        public RoleName FindById(int Id)
        {
            var p = new RoleName();
            try
            {
                using (var context = new GFDbContext())
                {
                    p = context.RoleName.SingleOrDefault(x => x.RoleID == Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }
    }
}
