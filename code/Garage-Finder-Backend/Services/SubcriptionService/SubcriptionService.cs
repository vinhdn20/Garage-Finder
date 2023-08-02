using AutoMapper;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Subscription;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.SubcriptionService
{
    public class SubcriptionService : ISubcriptionService
    {
        private readonly IConfiguration _config;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IMapper _mapper;
        public SubcriptionService(IConfiguration configuration, ISubscriptionRepository subscriptionRepository,
            IGarageRepository garageRepository, IMapper mapper) 
        {
            _config = configuration;
            _subscriptionRepository = subscriptionRepository;
            _garageRepository = garageRepository;
            _mapper = mapper;
        }
        public List<SubscribeDTO> GetAll()
        {
            var sub = _subscriptionRepository.GetAllSubscribe();
            //var subAvaiable = sub.Where(x => !x.Status.Equals(Constants.DELETE_SUBCRIPTION)).ToList();
            List<SubscribeDTO> result = new List<SubscribeDTO>();
            sub.ForEach(x => result.Add(_mapper.Map<SubscribeDTO>(x)));
            return result;
        }

        public void Add(AddSubcribeDTO addSubcribe)
        {
            var sub = _mapper.Map<Subscribe>(addSubcribe);
            sub.Status = Constants.OPEN_SUBCRIPTION;
            _subscriptionRepository.AddSubcribe(sub);
        }

        public void Update(SubscribeDTO subscribeDTO)
        {
            var sub = _mapper.Map<Subscribe>(subscribeDTO);
            var subDB= _subscriptionRepository.GetAllSubscribe().Find(x => x.SubscribeID == subscribeDTO.SubscribeID);
            if(subDB is null)
            {
                throw new Exception("Không tìm thấy gói đăng ký");
            }
            if(!sub.Status.Equals(Constants.OPEN_SUBCRIPTION) && !sub.Status.Equals(Constants.DELETE_SUBCRIPTION))
            {
                throw new Exception("Status không hợp lệ. Status phải là open hoặc block");
            }
            _subscriptionRepository.UpdateSubribe(sub);
        }

        public void Block(int subId)
        {
            var sub = _subscriptionRepository.GetById(subId);
            if(sub == null)
            {
                throw new Exception("Không thể tìm gói thành viên");
            }
            sub.Status = Constants.DELETE_SUBCRIPTION;
            _subscriptionRepository.UpdateSubribe(sub);
        }

        public void UpdateInvoice(VNPayIPNDTO vNPay)
        {
            int invoicesId = int.Parse(vNPay.vnp_TxnRef.Split('|')[0]);
            var invoices = _subscriptionRepository.GetInvoicesById(invoicesId);

            if (vNPay.vnp_ResponseCode.Equals(Constants.VNPAY_SUCCESS))
            {
                invoices.Status = Constants.INVOICE_PAID;
            }
            else
            {
                invoices.Status = Constants.INVOICE_FAIL;
            }

            _subscriptionRepository.UpdateInvoices(invoices);
        }

        public string GetLinkPay(int userId, int garageId, int subscriptionId, string ipAddress)
        {
            if(!ValidationGarageOwner(garageId, userId))
            {
                throw new Exception("Authorize exception");
            }

            var invoices =_subscriptionRepository.GetInvoicesByGarageId(garageId);
            //Tắt tạm
            //if(invoices.Any(x => x.ExpirationDate > DateTime.UtcNow.AddHours(7) && x.Status.Equals(Constants.INVOICE_PAID)))
            //{
            //    throw new Exception("Bạn đã đăng ký gói thành viên");
            //}
            var api = _config["VNPay:VNPayAPI"];
            var sub = _subscriptionRepository.GetById(subscriptionId);
            if(sub.Status.Equals(Constants.DELETE_SUBCRIPTION))
            {
                throw new Exception("Gói đăng ký không còn hiệu lực");
            }
            var garage = _garageRepository.GetGaragesByID(garageId);
            
            var amount = Convert.ToInt32(sub.Price * 100);
            var invoice = _subscriptionRepository.AddInVoices(new Invoices()
            {
                DateCreate = DateTime.UtcNow.AddHours(7),
                ExpirationDate = DateTime.UtcNow.AddHours(7 + sub.Period),
                Status = Constants.INVOICE_WAITING,
                GarageID = garageId,
                SubscribeID = subscriptionId,
            });
            VNPayPaymentDTO vNPay = new VNPayPaymentDTO()
            {
                vnp_Amount = amount,
                vnp_CreateDate = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                vnp_IpAddr = ipAddress,
                vnp_OrderInfo = $"Dang ky goi {sub.Name.convertToUnSign3()} cho garage {garage.GarageName.convertToUnSign3()}. So tien: {sub.Price}.",
                vnp_OrderType = "other",
                vnp_ReturnUrl = _config["VNPay:URLReturn"],
                vnp_TxnRef = invoice.InvoicesID.ToString(),
                vnp_TmnCode = _config["VNPay:vnp_TmnCode"],
                vnp_Version = _config["VNPay:API_Version"]
            };

            Dictionary<string,string> requestData = new Dictionary<string, string>();
            requestData.Add("vnp_Amount", vNPay.vnp_Amount.ToString());
            if(vNPay.vnp_BankCode != null)
            {
                requestData.Add("vnp_BankCode", vNPay.vnp_BankCode.ToString());
            }
            requestData.Add("vnp_Command", vNPay.vnp_Command);
            requestData.Add("vnp_CreateDate", vNPay.vnp_CreateDate.ToString());
            requestData.Add("vnp_CurrCode", vNPay.vnp_CurrCode.ToString());
            requestData.Add("vnp_IpAddr", vNPay.vnp_IpAddr);
            requestData.Add("vnp_Locale", vNPay.vnp_Locale);
            requestData.Add("vnp_OrderInfo", vNPay.vnp_OrderInfo);
            if (vNPay.vnp_OrderType != null)
            {
                requestData.Add("vnp_OrderType", vNPay.vnp_OrderType);
            }
            requestData.Add("vnp_ReturnUrl", vNPay.vnp_ReturnUrl);
            requestData.Add("vnp_TmnCode", vNPay.vnp_TmnCode);
            requestData.Add("vnp_TxnRef", vNPay.vnp_TxnRef + "|" + DateTime.Now.Ticks.ToString());
            requestData.Add("vnp_Version", vNPay.vnp_Version);
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            api += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {

                signData = signData.Remove(data.Length - 1, 1);
            }
            //vNPay.vnp_SecureHash = queryString.ToString().HmacSHA512(_config["VNPay:vnp_HashSecret"]);
            vNPay.vnp_SecureHash = signData.ToString().HmacSHA512(_config["VNPay:vnp_HashSecret"]);
            api += "vnp_SecureHash=" + vNPay.vnp_SecureHash;

            return api;
        }

        public List<InvoicesDTO> GetInvoicesByGarageId(int userId, int garageId)
        {
            if(!ValidationGarageOwner(garageId, userId))
            {
                throw new Exception("Authorize exception");
            }

            var invoices = _subscriptionRepository.GetInvoicesByGarageId(garageId);
            var viewInvoices = new List<InvoicesDTO>();
            var subs = _subscriptionRepository.GetAllSubscribe();
            foreach (var invoice in invoices)
            {
                InvoicesDTO invoicesDTO = _mapper.Map<InvoicesDTO>(invoice);
                var sub = subs.Find(x => x.SubscribeID == invoicesDTO.InvoicesID);
                invoicesDTO = _mapper.Map(sub,invoicesDTO);
                viewInvoices.Add(invoicesDTO);
            }
            return viewInvoices;
        }

        private bool ValidationGarageOwner(int garageId, int userId)
        {
            var garages = _garageRepository.GetGarageByUser(userId);
            if (!garages.Any(x => x.GarageID == garageId))
            {
                return false;
            }
            return true;
        }

        public void UnBlock(int subId)
        {
            var sub = _subscriptionRepository.GetById(subId);
            if (sub == null)
            {
                throw new Exception("Can not find subscription");
            }
            sub.Status = Constants.OPEN_SUBCRIPTION;
            _subscriptionRepository.UpdateSubribe(sub);
        }
    }
}
