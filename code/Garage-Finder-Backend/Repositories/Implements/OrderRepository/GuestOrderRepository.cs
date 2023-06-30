using AutoMapper;
using DataAccess.DAO;
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
    public class GuestOrderRepository : IGuestOrderRepository
    {
        private readonly IMapper _mapper;
        public GuestOrderRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Add(GuestOrderDTO order)
        {
            GuestOrderDAO.Instance.Add(_mapper.Map<GuestOrderDTO, GuestOrder>(order));
        }

        public void Delete(int id)
        {
            GuestOrderDAO.Instance.Delete(id);
        }
        public List<GuestOrderDTO> GetAllOrders()
        {
            return GuestOrderDAO.Instance.GetList().Select(p => _mapper.Map<GuestOrder, GuestOrderDTO>(p)).ToList();
        }

        public GuestOrderDTO GetOrderByGFId(int id)
        {
            return _mapper.Map<GuestOrder, GuestOrderDTO>(GuestOrderDAO.Instance.GetByGFId(id));
        }

        public GuestOrderDTO GetOrderById(int id)
        {
            return _mapper.Map<GuestOrder, GuestOrderDTO>(GuestOrderDAO.Instance.GetById(id));
        }

        public List<GuestOrderDTO> GetOrdersByGarageId(int id)
        {
            return GuestOrderDAO.Instance.GetByGarageId(id).Select(p => _mapper.Map<GuestOrder, GuestOrderDTO>(p)).ToList();
        }

        public void Update(GuestOrderDTO order)
        {
            GuestOrderDAO.Instance.Update(_mapper.Map<GuestOrderDTO, GuestOrder>(order));
        }
    }
}
