using GFData.Data;
using GFData.Models.Entity;

namespace DataAccess.DAO
{
    public class CarDAO
    {
        private static CarDAO instance = null;
        private static readonly object iLock = new object();
        public CarDAO()
        {

        }
        public static CarDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new CarDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Car> GetCars()
        {
            var listCategories = new List<Car>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listCategories = context.Cars.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategories;
        }

        public void Add(Car car)
        {
            try
            {
                var db = new GFDbContext();
                db.Cars.Add(car);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Car c)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Car>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteCar(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.Cars.SingleOrDefault(x => x.CarID == id);
                    context.Cars.Remove(cDelete);
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
