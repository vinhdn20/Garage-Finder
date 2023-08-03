using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RefreshTokenDAO
    {
        private static RefreshTokenDAO instance = null;
        private static readonly object iLock = new object();

        public RefreshTokenDAO()
        {

        }

        public static RefreshTokenDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new RefreshTokenDAO();
                    }
                    return instance;
                }
            }
        }

        public List<RefreshToken> GetRefreshTokenByUserID(int userID)
        {
            List<RefreshToken> refreshs = null;
            try
            {
                var db = new GFDbContext();
                refreshs = db.RefreshToken.Where(c => c.UserID == userID).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return refreshs;
        }

        public List<RefreshToken> GetRefreshTokenByTokenID(int tokenID)
        {
            List<RefreshToken> refreshs = null;
            try
            {
                var db = new GFDbContext();
                refreshs = db.RefreshToken.Where(c => c.TokenID == tokenID).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return refreshs;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                var db = new GFDbContext();
                db.RefreshToken.Add(refreshToken);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddOrUpdateRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                if(GetRefreshTokenByTokenID(refreshToken.TokenID) is null)
                {
                    AddRefreshToken(refreshToken);
                }
                else
                {
                    var db = new GFDbContext();
                    db.RefreshToken.Update(refreshToken);
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
                    var cDelete = context.RefreshToken.SingleOrDefault(x => x.UserID == userID);
                    context.RefreshToken.Remove(cDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteRefreshToKenExpired()
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    var cDelete = context.RefreshToken.Where(x => x.ExpiresDate <= DateTime.UtcNow);
                    context.RefreshToken.RemoveRange(cDelete);
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
