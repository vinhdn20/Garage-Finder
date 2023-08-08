using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Car;
using DataAccess.DTO.Orders;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.OrderRepository
{
    public class OrderRepository : IOderService
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

        public void AddOrderWithCar(OrdersDTO orders, CarDTO car)
        {
            OrdersDAO.Instance.AddOrderWithCar(_mapper.Map<OrdersDTO, Orders>(orders), _mapper.Map<CarDTO,Car>(car));
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

        public List<OrdersDTO> GetAllOrdersByGarageId(int id)
        {
            return OrdersDAO.Instance.GetListByGarageID(id).Select(p => _mapper.Map<Orders, OrdersDTO>(p)).ToList();
        }

        public void Update(OrdersDTO order)
        {
            OrdersDAO.Instance.Update(_mapper.Map<OrdersDTO, Orders>(order));
        }

        public OrdersDTO GetOrderByGFId(int id)
        {
            return _mapper.Map < Orders, OrdersDTO >(OrdersDAO.Instance.GetByGFId(id));
        }
    }
}
