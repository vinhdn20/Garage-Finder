using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FavoriteListDAO
    {
        private static FavoriteListDAO instance = null;
        private static readonly object iLock = new object();
        public FavoriteListDAO()
        {

        }
        public static FavoriteListDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new FavoriteListDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Garage> GetList(int id)
        {
            var favoriteList = new List<Garage>();
            try
            {
                using (var context = new GFDbContext())
                {
                    favoriteList = (from garage in context.Garage
                                    join favorite in context.FavoriteList on garage.GarageID equals favorite.GarageID
                                    where favorite.UserID == id
                                    select garage).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return favoriteList;
        }

        public void SaveList(FavoriteList favoriteList)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.FavoriteList.Add(favoriteList);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteByGarageId(int garageId, int userId)
        {
            {
                try
                {
                    using (var context = new GFDbContext())
                    {
                        var cDelete = context.FavoriteList.SingleOrDefault(x => x.GarageID == garageId && x.UserID == userId);
                        context.FavoriteList.Remove(cDelete);
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
}
