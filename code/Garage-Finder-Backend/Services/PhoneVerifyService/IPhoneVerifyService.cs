using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PhoneVerifyService
{
    public interface IPhoneVerifyService
    {
        Task<bool> SendCodeAsync(string phoneNumber);
        Task<bool> VerifyPhoneNumber(string verificationCode, string phoneNumber);
    }
}
