using AutoMapper;
using DataAccess.DTO.Token;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RefreshTokenService
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IMapper mapper, IRefreshTokenRepository refreshTokenRepository)
        {
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public void AddOrUpdateToken(RefreshTokenDTO refreshToken)
        {
            _refreshTokenRepository.AddOrUpdateToken(refreshToken);
        }

        public List<RefreshTokenDTO> GetRefreshToken(int userID)
        {
            List<RefreshTokenDTO> refreshTokens = new List<RefreshTokenDTO>();
            _refreshTokenRepository.GetRefreshToken(userID);
            return refreshTokens;
        }

        public void DeleteRefreshToken(int userID)
        {
            _refreshTokenRepository.DeleteRefreshToken(userID);
        }

        public void DeleteRefreshToKenExpired()
        {
            _refreshTokenRepository.DeleteRefreshToKenExpired();
        }

        public List<RefreshTokenDTO> GetAll()
        {
            List<RefreshTokenDTO> refreshTokens = new List<RefreshTokenDTO>();
            _refreshTokenRepository.GetAll();
            return refreshTokens;
        }
    }
}
