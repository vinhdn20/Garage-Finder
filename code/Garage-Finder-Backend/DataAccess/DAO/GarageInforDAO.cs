using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class GarageInforDAO
    {
        private static GarageInforDAO instance = null;
        private static readonly object iLock = new object();

        public GarageInforDAO()
        {

        }

        public static GarageInforDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new GarageInforDAO();
                    }
                    return instance;
                }
            }
        }

        public void Add(GarageInfo garageInfo)
        {
            try
            {
                var db = new GFDbContext();
                db.GarageInfo.Add(garageInfo);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
