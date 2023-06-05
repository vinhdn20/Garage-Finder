using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class GarageBrandDAO
    {
        private static GarageBrandDAO instance = null;
        private static readonly object iLock = new object();
        public GarageBrandDAO()
        {

        }

        public static GarageBrandDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new GarageBrandDAO();
                    }
                    return instance;
                }
            }
        }

        public List<GarageBrand> GetBrand()
        {
            var listBrands = new List<GarageBrand>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listBrands = context.GarageBrand.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listBrands;
        }

        public void Add(GarageBrand brand)
        {
            try
            {
                var db = new GFDbContext();
                db.GarageBrand.Add(brand);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(GarageBrand c)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<GarageBrand>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                    var cDelete = context.GarageBrand.SingleOrDefault(x => x.BrandID == id);
                    context.GarageBrand.Remove(cDelete);
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
