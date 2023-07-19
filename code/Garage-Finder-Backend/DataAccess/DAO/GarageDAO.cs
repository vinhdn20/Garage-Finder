using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Transactions;

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
                    listGarage = (from garage in context.Garage
                                  where garage.Status != Constants.DELETE_GARAGE
                                 select new Garage
                                 {
                                     GarageID = garage.GarageID,
                                     AddressDetail = garage.AddressDetail,
                                     OpenTime = garage.OpenTime,
                                     CloseTime = garage.CloseTime,
                                     DistrictsID = garage.DistrictsID,
                                     EmailAddress = garage.EmailAddress,
                                     GarageName = garage.GarageName,
                                     LatAddress = garage.LatAddress,
                                     LngAddress = garage.LngAddress,
                                     Thumbnail = garage.Thumbnail,
                                     PhoneNumber = garage.PhoneNumber,
                                     ProvinceID = garage.ProvinceID,
                                     Status = garage.Status,
                                     UserID = garage.UserID,
                                     ImageGarages = (from imageGarage in context.ImageGarage
                                                     where imageGarage.GarageID == garage.GarageID
                                                     select imageGarage).ToList(),
                                     //CategoryGarages = context.CategoryGarage.Where(x => x.GarageID == garage.GarageID).ToList(),
                                     CategoryGarages = (from cateGara in context.CategoryGarage
                                                        where cateGara.GarageID == garage.GarageID
                                                        select new CategoryGarage
                                                        {
                                                            CategoryGarageID = cateGara.CategoryGarageID,
                                                            CategoryID = cateGara.CategoryID,
                                                            GarageID = garage.GarageID,
                                                            Services = context.Service
                                                                .Where(x => x.CategoryGarageID == cateGara.CategoryGarageID).ToList()
                                                        }).ToList(),
                                     GarageBrands = context.GarageBrand.Where(x => x.GarageID == garage.GarageID).ToList()
                                 }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listGarage;
        }

        public List<Garage> GetByUserID(int userID)
        {
            var listGarage = new List<Garage>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listGarage = (from garage in context.Garage
                                  where garage.UserID == userID && garage.Status != Constants.DELETE_GARAGE
                                  select new Garage
                                  {
                                      GarageID = garage.GarageID,
                                      AddressDetail = garage.AddressDetail,
                                      OpenTime = garage.OpenTime,
                                      CloseTime = garage.CloseTime,
                                      DistrictsID = garage.DistrictsID,
                                      EmailAddress = garage.EmailAddress,
                                      GarageName = garage.GarageName,
                                      LatAddress = garage.LatAddress,
                                      LngAddress = garage.LngAddress,
                                      Thumbnail = garage.Thumbnail,
                                      PhoneNumber = garage.PhoneNumber,
                                      ProvinceID = garage.ProvinceID,
                                      Status = garage.Status,
                                      UserID = garage.UserID,
                                      ImageGarages = (from imageGarage in context.ImageGarage
                                                      where imageGarage.GarageID == garage.GarageID
                                                      select imageGarage).ToList(),
                                      CategoryGarages = (from cateGara in context.CategoryGarage
                                                         where cateGara.GarageID == garage.GarageID
                                                         select new CategoryGarage
                                                         {
                                                             CategoryGarageID = cateGara.CategoryGarageID,
                                                             CategoryID = cateGara.CategoryID,
                                                             GarageID = garage.GarageID,
                                                             Services = context.Service
                                                                 .Where(x => x.CategoryGarageID == cateGara.CategoryGarageID).ToList()
                                                         }).ToList(),
                                      GarageBrands = context.GarageBrand.Where(x => x.GarageID == garage.GarageID).ToList()
                                  }).ToList();
                                  
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async Task<Garage> SaveGrageWithInforAsync(Garage garage, List<GarageBrand> garageBrands,
            List<CategoryGarage> categoryGarages, List<ImageGarage> imageGarages)
        {
            using var context = new GFDbContext();
            var transaction = context.Database.CreateExecutionStrategy();
            await transaction.ExecuteAsync(async () =>
            {
                using(var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Garage.Add(garage);
                        context.SaveChanges();
                        foreach (var garagebrand in garageBrands)
                        {
                            garagebrand.GarageID = garage.GarageID;
                            context.GarageBrand.Add(garagebrand);
                        }
                        foreach (var category in categoryGarages)
                        {
                            category.GarageID = garage.GarageID;
                            context.CategoryGarage.Add(category);
                        }

                        foreach (var image in imageGarages)
                        {
                            image.GarageID = garage.GarageID;
                            context.ImageGarage.Add(image);
                        }
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return garage;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            });
            return null;
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
                    cDelete.Status = Constants.DELETE_GARAGE;
                    context.Garage.Update(cDelete);
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
