using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        public void AddOrUpdateToken(RefreshTokenDTO refreshToken)
        {
            RefreshTokenDAO.Instance.AddOrUpdateRefreshToken(Mapper.mapToEntity(refreshToken));
        }

        public List<RefreshTokenDTO> GetRefreshToken(int userID)
        {
            List<RefreshTokenDTO> refreshTokens = new List<RefreshTokenDTO>();
            RefreshTokenDAO.Instance.GetRefreshTokenByUserID(userID).ForEach(x => refreshTokens.Add(Mapper.mapToDTO(x)));
            return refreshTokens;
        }
    }
}
