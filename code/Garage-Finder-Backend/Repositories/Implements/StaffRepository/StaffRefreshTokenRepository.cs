using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Token;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.UserRepository
{

    public class StaffRefreshTokenRepository : IStaffRefreshTokenRepository
    {
        public StaffRefreshTokenRepository()
        {
        }
        public void AddOrUpdateToken(StaffRefreshToken refreshToken)
        {
            StaffRefreshTokenDAO.Instance.AddOrUpdateRefreshToken(refreshToken);
        }

        public List<StaffRefreshToken> GetRefreshToken(int userID)
        {
            List<StaffRefreshToken> refreshTokens = new List<StaffRefreshToken>();
            StaffRefreshTokenDAO.Instance.GetRefreshTokenByUserID(userID).ForEach(x => refreshTokens.Add(x));
            return refreshTokens;
        }

        public void DeleteRefreshToken(int userID)
        {
            StaffRefreshTokenDAO.Instance.DeleteRefreshTokenByUserID(userID);
        }
    }
}
