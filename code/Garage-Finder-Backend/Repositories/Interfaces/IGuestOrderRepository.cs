using DataAccess.DTO.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IGuestOrderRepository
    {
        List<GuestOrderDTO> GetAllOrders();
        List<GuestOrderDTO> GetOrdersByGarageId(int id);
        GuestOrderDTO GetOrderById(int id);
        GuestOrderDTO GetOrderByGFId(int id);
        void Add(GuestOrderDTO order);
        void Update(GuestOrderDTO order);
        void Delete(int id);
    }
}
