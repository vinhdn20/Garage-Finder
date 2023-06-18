using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
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

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMapper _mapper;
        public RefreshTokenRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void AddOrUpdateToken(RefreshTokenDTO refreshToken)
        {
            RefreshTokenDAO.Instance.AddOrUpdateRefreshToken(_mapper.Map<RefreshTokenDTO, RefreshToken>(refreshToken));
        }

        public List<RefreshTokenDTO> GetRefreshToken(int userID)
        {
            List<RefreshTokenDTO> refreshTokens = new List<RefreshTokenDTO>();
            RefreshTokenDAO.Instance.GetRefreshTokenByUserID(userID).ForEach(x => refreshTokens.Add(_mapper.Map<RefreshToken, RefreshTokenDTO>(x)));
            return refreshTokens;
        }

        public void DeleteRefreshToken(int userID)
        {
            RefreshTokenDAO.Instance.DeleteRefreshTokenByUserID(userID);
        }
    }
}
