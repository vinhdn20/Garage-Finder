using DataAccess.DTO.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SubcriptionService
{
    public interface ISubcriptionService
    {
        public void UpdateInvoice(VNPayIPNDTO vNPay);
        public string GetLinkPay(int userId, int garageId, int subscriptionId, string ipAddress);
        List<SubscribeDTO> GetAll();
        void Add(AddSubcribeDTO addSubcribe);
        void Update(SubscribeDTO subscribeDTO);
        void Block(int subId);
        void UnBlock(int subId);
        public List<InvoicesDTO> GetInvoicesByGarageId(int userId, int garageId);
    }
}
