using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class StaffRefreshTokenDAO
    {
        private static StaffRefreshTokenDAO instance = null;
        private static readonly object iLock = new object();

        public StaffRefreshTokenDAO()
        {

        }

        public static StaffRefreshTokenDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new StaffRefreshTokenDAO();
                    }
                    return instance;
                }
            }
        }

        public List<StaffRefreshToken> GetRefreshTokenByUserID(int userID)
        {
            List<StaffRefreshToken> refreshs = null;
            try
            {
                var db = new GFDbContext();
                refreshs = db.StaffRefreshToken.Where(c => c.StaffId == userID).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return refreshs;
        }

        public List<StaffRefreshToken> GetRefreshTokenByTokenID(int tokenID)
        {
            List<StaffRefreshToken> refreshs = null;
            try
            {
                var db = new GFDbContext();
                refreshs = db.StaffRefreshToken.Where(c => c.TokenID == tokenID).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return refreshs;
        }

        public void AddRefreshToken(StaffRefreshToken refreshToken)
        {
            try
            {
                var db = new GFDbContext();
                db.StaffRefreshToken.Add(refreshToken);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddOrUpdateRefreshToken(StaffRefreshToken refreshToken)
        {
            try
            {
                if (GetRefreshTokenByTokenID(refreshToken.TokenID) is null)
                {
                    AddRefreshToken(refreshToken);
                }
                else
                {
                    var db = new GFDbContext();
                    db.StaffRefreshToken.Update(refreshToken);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteRefreshTokenByUserID(int userID)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.StaffRefreshToken.SingleOrDefault(x => x.StaffId == userID);
                    context.StaffRefreshToken.Remove(cDelete);
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
