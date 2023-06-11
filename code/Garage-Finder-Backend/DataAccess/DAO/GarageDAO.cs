using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class GarageDAO
    {
        private static GarageDAO instance = null;
        private static readonly object iLock = new object();
        public GarageDAO()
        {

        }

        public static GarageDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new GarageDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Garage> GetGarages()
        {
            var listGarage = new List<Garage>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listGarage = context.Garage.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listGarage;
        }

        public List<Garage> FindAll()
        {
            var p = new List<Garage>();
            try
            {
                using (var context = new GFDbContext())
                {
                    p = context.Garage.Include(m => m.GarageID).ToList();

                    if (p == null)
                    {
                        throw new Exception("No Garages!");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public Garage SaveGarage(Garage garage)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Garage.Add(garage);
                    context.SaveChanges();
                }
                return garage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateGarage(Garage garage)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Garage>(garage).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteGarage(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.Garage.SingleOrDefault(x => x.GarageID == id);
                    context.Garage.Remove(cDelete);
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
