using DataAccess.DTO.Subscription;
using GFData.Models.Entity;
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
        private readonly IGarageRepository _garageRepository;
        public SubcriptionService(IConfiguration configuration, ISubscriptionRepository subscriptionRepository,
            IGarageRepository garageRepository) 
        {
            _config = configuration;
            _subscriptionRepository = subscriptionRepository;
            _garageRepository = garageRepository;
        }
        public void AddInvoice()
        {
            throw new NotImplementedException();
        }

        public string GetLinkPay(int userId, int garageId, int subscriptionId, string ipAddress)
        {
            var api = _config["VNPay:VNPayAPI"];
            var sub = _subscriptionRepository.GetById(subscriptionId);
            var garage = _garageRepository.GetGaragesByID(garageId);
            
            var amount = Convert.ToInt32(sub.Price * 100);
            var invoice = _subscriptionRepository.AddInVoices(new Invoices()
            {
                DateCreate = DateTime.UtcNow.AddHours(7),
                ExpirationDate = DateTime.UtcNow.AddHours(7 + sub.Period),
                Status = Constants.INVOICE_WAITING,
                UserID = userId,
                SubscribeID = subscriptionId,
            });
            VNPayPaymentDTO vNPay = new VNPayPaymentDTO()
            {
                vnp_Amount = amount,
                vnp_CreateDate = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                vnp_IpAddr = ipAddress,
                vnp_OrderInfo = $"Dang ky goi {sub.Name} cho garage {garage.GarageName}. So tien: {sub.Price}.",
                vnp_OrderType = "other",
                vnp_ReturnUrl = _config["VNPay:URLReturn"],
                vnp_TxnRef = invoice.InvoicesID.ToString(),
                vnp_TmnCode = _config["VNPay:vnp_TmnCode"],
                vnp_Version = _config["VNPay:API_Version"]
            };

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("vnp_Amount", vNPay.vnp_Amount.ToString());
            if(vNPay.vnp_BankCode != null)
            {
                queryString.Add("vnp_BankCode", vNPay.vnp_BankCode.ToString());
            }
            queryString.Add("vnp_Command", vNPay.vnp_Command);
            queryString.Add("vnp_CreateDate", vNPay.vnp_CreateDate.ToString());
            queryString.Add("vnp_CurrCode", vNPay.vnp_CurrCode.ToString());
            queryString.Add("vnp_IpAddr", vNPay.vnp_IpAddr);
            queryString.Add("vnp_Locale", vNPay.vnp_Locale);
            queryString.Add("vnp_OrderInfo", vNPay.vnp_OrderInfo);
            if (vNPay.vnp_OrderType != null)
            {
                queryString.Add("vnp_OrderType", vNPay.vnp_OrderType);
            }
            queryString.Add("vnp_ReturnUrl", vNPay.vnp_ReturnUrl);
            queryString.Add("vnp_TmnCode", vNPay.vnp_TmnCode);
            queryString.Add("vnp_TxnRef", vNPay.vnp_TxnRef);
            queryString.Add("vnp_Version", vNPay.vnp_Version);

            vNPay.vnp_SecureHash = queryString.ToString().HmacSHA512(_config["VNPay:vnp_HashSecret"]);
            queryString.Add("vnp_SecureHash", vNPay.vnp_SecureHash);
            api += "?" + queryString.ToString();
            return api;
        }

        public void Vnpay_ipn()
        {
            throw new NotImplementedException();
        }
    }
}
