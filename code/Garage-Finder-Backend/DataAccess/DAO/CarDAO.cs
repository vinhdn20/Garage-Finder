using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;

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

        //public List<Car> GetCars()
        //{
        //    var listCars = new List<Car>();
        //    try
        //    {
        //        using (var context = new GFDbContext())
        //        {
        //            listCars = context.Car.ToList();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return listCars;
        //}

        public List<Car> GetCars()
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var listCars = (from car in context.Car
                                join imageCar in context.ImageCar on car.CarID equals imageCar.CarID into imageCars                                
                                select new Car
                                {
                                    CarID = car.CarID,
                                    UserID = car.UserID,
                                    LicensePlates = car.LicensePlates,
                                    BrandID = car.BrandID,
                                    Color = car.Color,
                                    TypeCar = car.TypeCar
                                });
                    return new List<Car>(listCars);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(Car car)
        {
            try
            {
                var db = new GFDbContext();
                db.Car.Add(car);
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
        public void SaveCar(Car p)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Car.Add(p);
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
                    var cDelete = context.Car.SingleOrDefault(x => x.CarID == id);
                    context.Car.Remove(cDelete);
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
