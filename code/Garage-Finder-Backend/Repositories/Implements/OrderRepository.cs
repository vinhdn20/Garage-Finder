using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        public OrderRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(OrdersDTO order)
        {
            OrdersDAO.Instance.Add(_mapper.Map<OrdersDTO, Orders>(order));
        }

        public void Delete(int id)
        {
            OrdersDAO.Instance.Delete(id);
        }
        public List<OrdersDTO> GetAllOrders()
        {
            return OrdersDAO.Instance.GetList().Select(p => _mapper.Map<Orders, OrdersDTO>(p)).ToList();
        }

        public List<OrdersDTO> GetAllOrdersByUserId(int id)
        {
            //return OrdersDAO.Instance.GetList().Where(c => c.CarID == id).Select(p => _mapper.Map<Orders, OrdersDTO>(p)).ToList();
            return OrdersDAO.Instance.GetListByUserID(id).Select(p => _mapper.Map<Orders, OrdersDTO>(p)).ToList();
        }

        public OrdersDTO GetOrderById(int id)
        {
            return _mapper.Map<Orders, OrdersDTO>(OrdersDAO.Instance.GetById(id));
        }

        public void Update(OrdersDTO order)
        {
            OrdersDAO.Instance.Update(_mapper.Map<OrdersDTO, Orders>(order));
        }
    }
}
