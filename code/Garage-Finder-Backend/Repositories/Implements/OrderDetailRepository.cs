using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Add(OrderDetailDTO orderDetail)
        {
            OrderDetailDAO.Instance.Add(Mapper.mapToEntity(orderDetail));
        }

        public void Delete(int id)
        {
            OrderDetailDAO.Instance.Delete(id);
        }

        public OrderDetailDTO GetOrderDetailByOrderID(int orderID)
        {
            return Mapper.mapToDTO(OrderDetailDAO.Instance.GetById(orderID));
        }

        public void Update(OrderDetailDTO orderDetail)
        {
            OrderDetailDAO.Instance.Update(Mapper.mapToEntity(orderDetail));
        }
    }
}
