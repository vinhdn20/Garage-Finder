using DataAccess.DTO.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RefreshTokenService
{
    public interface IRefreshTokenService
    {
        void AddOrUpdateToken(RefreshTokenDTO refreshToken);
        List<RefreshTokenDTO> GetRefreshToken(int userID);
        void DeleteRefreshToken(int userID);
        void DeleteRefreshToKenExpired();
        List<RefreshTokenDTO> GetAll();
    }
}
