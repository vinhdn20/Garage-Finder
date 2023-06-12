using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ImageGarageDAO
    {
        private static ImageGarageDAO instance = null;
        private static readonly object iLock = new object();

        public ImageGarageDAO()
        {

        }

        public static ImageGarageDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new ImageGarageDAO();
                    }
                    return instance;
                }
            }
        }

        public void Add(ImageGarage imageGarage)
        {
            try
            {
                var db = new GFDbContext();
                db.ImageGarage.Add(imageGarage);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
