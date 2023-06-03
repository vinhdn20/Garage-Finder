using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void AddOrUpdateToken(RefreshTokenDTO refreshToken);
        List<RefreshTokenDTO> GetRefreshToken(int userID);
        void DeleteRefreshToken(int userID);
    }
}
