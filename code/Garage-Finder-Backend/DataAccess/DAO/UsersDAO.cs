using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DAO
{
    public class UsersDAO
    {
        private static UsersDAO instance = null;
        private static readonly object iLock = new object();
        public UsersDAO()
        {

        }

        public static UsersDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new UsersDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Users> FindAll()
        {
            var p = new List<Users>();
            try
            {
                using (var context = new GFDbContext())
                {
                    p = context.Users.Include(m => m.RoleName).ToList();

                    if (p == null)
                    {
                        throw new Exception("No Users!");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public Users FindUserByEmailPassword(string email, string password)
        {
            var p = new Users();
            try
            {
                if (email.Equals("")) throw new Exception("Email must not be empty");
                if (password.Equals("")) throw new Exception("Password must not be empty");
                using (var context = new GFDbContext())
                {
                    p = context.Users.Include(m => m.RoleName).SingleOrDefault(x => x.EmailAddress == email && x.Password == password);

                    if (p == null)
                    {
                        throw new Exception("Wrong password or username");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public void SaveUser(Users user)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateUser(Users user)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Users>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
