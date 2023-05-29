using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object iLock = new object();
        public CategoryDAO()
        {

        }

        public static CategoryDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Categorys> GetCategories()
        {
            var listCategories = new List<Categorys>();
            try
            {
                using (var context = new GFDbContext())
                {
                    listCategories = context.Category.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategories;
        }

        public void Add(Categorys category)
        {
            try
            {
                var db = new GFDbContext();
                db.Category.Add(category);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Categorys c)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Categorys>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.Category.SingleOrDefault(x => x.CategoryID == id);
                    context.Category.Remove(cDelete);
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
