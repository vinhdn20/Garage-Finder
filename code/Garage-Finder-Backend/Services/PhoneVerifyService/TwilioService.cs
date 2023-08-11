using GFData.Models.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;

namespace Services.PhoneVerifyService
{
    public class TwilioService : IPhoneVerifyService
    {
        private readonly TwilioVerifySettings _settings;
        private readonly IConfiguration _configuration;
        public TwilioService(IConfiguration configuration, IOptions<TwilioVerifySettings> settings)
        {
            _configuration = configuration;
            _settings = settings.Value;
        }
        public async Task<bool> SendCodeAsync(string phoneNumber)
        {
            try
            {
                //return true;
                phoneNumber = ChangePhoneToVNPhone(phoneNumber);
                var verification = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: "sms",
                    pathServiceSid: _settings.VerificationServiceSID
                );
                if (verification.Status == "pending")
                {
                    return true;
                }
                throw new Exception($"There was an error sending the verification code: {verification}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerifyPhoneNumber(string verificationCode, string phoneNumber)
        {
            try
            {
                //return true;
                phoneNumber = ChangePhoneToVNPhone(phoneNumber);
                var verification = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: verificationCode,
                    pathServiceSid: _settings.VerificationServiceSID
                );
                if (verification.Status == "approved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ChangePhoneToVNPhone(string phoneNumber)
        {
            return "+84" + phoneNumber.Substring(1);
        }
    }
}
