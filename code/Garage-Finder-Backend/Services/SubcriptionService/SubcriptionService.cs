using DataAccess.DTO.Subscription;
using Microsoft.Extensions.Configuration;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SubcriptionService
{
    public class SubcriptionService : ISubcriptionService
    {
        private readonly IConfiguration _config;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public SubcriptionService(IConfiguration configuration, ISubscriptionRepository subscriptionRepository) 
        {
            _config = configuration;
            _subscriptionRepository = subscriptionRepository;
        }
        public void AddInvoice()
        {
            throw new NotImplementedException();
        }

        public string GetLinkPay(int subscriptionId)
        {
            var api = _config["VNPay:VNPayAPI"];
            var sub = _subscriptionRepository.GetById(subscriptionId);
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            VNPayPaymentDTO vNPay = new VNPayPaymentDTO()
            {
                vnp_Amount = Convert.ToInt32(sub.Price * 100),
                
            };
            return "";
        }

        public void Vnpay_ipn()
        {
            throw new NotImplementedException();
        }
    }
}
