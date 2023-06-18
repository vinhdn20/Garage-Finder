using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

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
                    //p = context.Users.Include(m => m.RoleName).ToList();
                    p = context.User.ToList();
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
                    //p = context.Users.Include(m => m.RoleName).SingleOrDefault(x => x.EmailAddress == email && x.Password == password);
                    p = context.User.SingleOrDefault(x => x.EmailAddress == email && x.Password == password);

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

        public Users FindUserByEmail(string email)
        {
            var p = new Users();
            try
            {
                if (email.Equals("")) throw new Exception("Email must not be empty");
                using (var context = new GFDbContext())
                {
                    p = context.User.SingleOrDefault(x => x.EmailAddress == email);

                    if (p == null)
                    {
                        //disable this exception to allow register new user
                        //throw new Exception("Wrong username");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public Users FindUserByPhone(string phone)
        {
            var p = new Users();
            try
            {
                if (phone.Equals("")) throw new Exception("Email must not be empty");
                using (var context = new GFDbContext())
                {
                    p = (from user in context.User
                         where user.PhoneNumber.Equals(phone)
                         select new Users
                         {
                             UserID = user.UserID,
                             EmailAddress = user.EmailAddress,
                             PhoneNumber = user.PhoneNumber,
                             Password = user.Password,
                             LinkImage = user.LinkImage,
                             RoleID = user.RoleID,
                             RoleName = context.RoleName.Where(x => x.RoleID == user.RoleID).FirstOrDefault(),
                             Status = user.Status,
                             Name = user.Name,
                             DistrictId = user.DistrictId,
                             ProvinceId = user.ProvinceId,
                             AddressDetail = user.AddressDetail,
                         }).FirstOrDefault();

                    if (p == null)
                    {
                        throw new Exception("Wrong phoneNumber");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public Users FindUserByID(int id)
        {
            var p = new Users();
            try
            {
                using(var context = new GFDbContext())
                {
                    p = (from user in context.User
                        where user.UserID == id
                        select new Users
                        {
                            UserID = user.UserID,
                            EmailAddress = user.EmailAddress, 
                            PhoneNumber = user.PhoneNumber,
                            Password = user.Password,
                            LinkImage = user.LinkImage,
                            RoleID = user.RoleID,
                            RoleName = context.RoleName.Where(x => x.RoleID == user.RoleID).FirstOrDefault(),
                            Status = user.Status,
                            Name = user.Name,
                            DistrictId = user.DistrictId,
                            ProvinceId = user.ProvinceId,
                            AddressDetail = user.AddressDetail,
                        }).FirstOrDefault();
                    if (p == null)
                    {
                        throw new Exception("Wrong id");
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
                    context.User.Add(user);
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
                    context.User.Update(user);
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
