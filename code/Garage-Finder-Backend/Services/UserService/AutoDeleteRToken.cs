using Quartz;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    [DisallowConcurrentExecution]
    public class AutoDeleteRToken : IJob
    {
        private readonly IRefreshTokenRepository _refreshToken;
        public AutoDeleteRToken(IRefreshTokenRepository refreshTokenRepository) 
        {
            _refreshToken = refreshTokenRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _refreshToken.DeleteRefreshToKenExpired();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
            
        }
    }
}
