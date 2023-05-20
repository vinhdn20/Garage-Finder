using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderDetailRepository
    {
        OrderDetailDTO GetOrderDetailByOrderID(int orderID);
        void Add(OrderDetailDTO orderDetail);
        void Update(OrderDetailDTO orderDetail);
        void Delete(int id);
    }
}
