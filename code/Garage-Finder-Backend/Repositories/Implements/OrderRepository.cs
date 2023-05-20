using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class OrderRepository : IOrderRepository
    {
        public void Add(OrdersDTO order)
        {
            OrdersDAO.Instance.Add(Mapper.mapToEntity(order));
        }

        public void Delete(int id)
        {
            OrdersDAO.Instance.Delete(id);
        }

        public IEnumerable<OrdersDTO> GetAllOrders()
        {
            return OrdersDAO.Instance.GetList().Select(p => Mapper.mapToDTO(p)).ToList();
        }

        public IEnumerable<OrdersDTO> GetAllOrdersByUserId(int id)
        {
            return OrdersDAO.Instance.GetList().Where(c => c.CarID == id).Select(p => Mapper.mapToDTO(p)).ToList();
        }

        public OrdersDTO GetOrderById(int id)
        {
            return Mapper.mapToDTO(OrdersDAO.Instance.GetById(id));
        }

        public void Update(OrdersDTO order)
        {
            OrdersDAO.Instance.Update(Mapper.mapToEntity(order));
        }
    }
}
