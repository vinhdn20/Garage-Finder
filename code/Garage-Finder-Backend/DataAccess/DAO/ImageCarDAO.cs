using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ImageCarDAO
    {
        private static ImageCarDAO instance = null;
        private static readonly object iLock = new object();

        public ImageCarDAO()
        {

        }

        public static ImageCarDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new ImageCarDAO();
                    }
                    return instance;
                }
            }
        }

        public void Add(ImageCar imageCar)
        {
            try
            {
                var db = new GFDbContext();
                db.ImageCar.Add(imageCar);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                var db = new GFDbContext();
                var image = db.ImageCar.SingleOrDefault(x => x.ImageId == id);
                if (image != null)
                {
                    db.ImageCar.Remove(image);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                throw;
            }
        }
    }
}
