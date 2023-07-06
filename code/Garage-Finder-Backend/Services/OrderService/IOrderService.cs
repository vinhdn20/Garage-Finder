using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.RequestDTO;
using DataAccess.DTO.Orders.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public interface IOrderService
    {
        OrderDetailDTO GetOrderByGFID(int gfid, int userId);
        List<OrderDetailDTO> GetOrderByGarageId(int garageId, int userId);
        void AddOrderWithCar(AddOrderWithCarDTO addOrder);
        void AddOrderFromGuest(AddOrderFromGuestDTO addOrder);
        void AddOrderWithoutCar(AddOrderWithoutCarDTO addOrder, int userID);
        void GarageAcceptOrder(int GFId, int userId);
        void GarageRejectOrder(int GFId, int userId);
        void GarageCancelOrder(int GFId, int userId);
        void GarageDoneOrder(DoneOrderDTO doneOrder, int userId);
        void UserCancelOrder(int userId, int GFId);
    }
}
