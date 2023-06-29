using DataAccess.DTO.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        List<OrdersDTO> GetAllOrders();
        List<OrdersDTO> GetAllOrdersByUserId(int id);
        List<OrdersDTO> GetAllOrdersByGarageId(int id);
        OrdersDTO GetOrderById(int id);
        OrdersDTO GetOrderByGFId(int id);
        void Add(OrdersDTO order);
        void Update(OrdersDTO order);
        void Delete(int id);
    }
}
