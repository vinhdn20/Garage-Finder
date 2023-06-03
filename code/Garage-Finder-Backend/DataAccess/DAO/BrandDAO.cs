using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BrandDAO
    {
        private static BrandDAO instance = null;
        private static readonly object iLock = new object();
        public BrandDAO()
        {

        }

        public static BrandDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new BrandDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Brand> GetBrand()
        {
            var listBrands = new List<Brand>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listBrands = context.Brand.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listBrands;
        }

        public void Add(Brand brand)
        {
            try
            {
                var db = new GFDbContext();
                db.Brand.Add(brand);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Brand c)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Brand>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.Brand.SingleOrDefault(x => x.BrandID == id);
                    context.Brand.Remove(cDelete);
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
