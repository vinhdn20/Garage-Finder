using DataAccess.DTO.Token;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStaffRefreshTokenRepository
    {
        void AddOrUpdateToken(StaffRefreshToken refreshToken);
        List<StaffRefreshToken> GetRefreshToken(int userID);
        void DeleteRefreshToken(int userID);
    }
}
