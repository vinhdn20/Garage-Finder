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
        public List<FavoriteList> GetList()
        {
            var favoriteList = new List<FavoriteList>();
            try
            {
                using (var context = new GFDbContext())
                {
                    favoriteList = context.FavoriteList.ToList();
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

        public void Delete(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.FavoriteList.SingleOrDefault(x => x.FavoriteID == id);
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
