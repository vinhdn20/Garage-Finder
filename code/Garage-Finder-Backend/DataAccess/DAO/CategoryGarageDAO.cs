using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryGarageDAO
    {
        private static CategoryGarageDAO instance = null;
        private static readonly object iLock = new object();

        public CategoryGarageDAO()
        {

        }

        public static CategoryGarageDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryGarageDAO();
                    }
                    return instance;
                }
            }
        }

        public void Add(CategoryGarage categoryGarage)
        {
            try
            {
                var db = new GFDbContext();
                db.CategoryGarage.Add(categoryGarage);
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
                var cate = db.CategoryGarage.FirstOrDefault(x => x.CategoryGarageID == id);
                if (cate != null)
                {
                    var services = db.Service.Where(x => x.CategoryGarageID == id).ToList();
                    var orderDetails = db.OrderDetail.Where(x => x.CategoryGarageID == id).ToList();
                    var guestOrderDetails = db.GuestOrderDetail.Where(x => x.CategoryGarageID == id).ToList();
                    foreach ( var service in services)
                    {
                        db.Service.Remove(service);
                    }

                    foreach (var order in orderDetails)
                    {
                        db.OrderDetail.Remove(order);
                    }

                    foreach (var order in guestOrderDetails)
                    {
                        db.GuestOrderDetail.Remove(order);
                    }
                    db.CategoryGarage.Remove(cate);
                    db.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CategoryGarage GetByID(int id)
        {
            try
            {
                var db = new GFDbContext();
                var cate = db.CategoryGarage.FirstOrDefault(x => x.CategoryGarageID == id);
                return cate;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
