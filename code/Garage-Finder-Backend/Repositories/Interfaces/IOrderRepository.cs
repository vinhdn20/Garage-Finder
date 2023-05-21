using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<OrdersDTO> GetAllOrders();
        IEnumerable<OrdersDTO> GetAllOrdersByUserId(int id);
        OrdersDTO GetOrderById(int id);
        void Add(OrdersDTO order);
        void Update(OrdersDTO order);
        void Delete(int id);
    }
}
