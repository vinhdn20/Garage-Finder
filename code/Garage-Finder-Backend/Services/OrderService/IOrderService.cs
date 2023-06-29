using DataAccess.DTO.Orders.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public interface IOrderService
    {
        void AddOrderWithCar(AddOrderWithCarDTO addOrder);
        void AddOrderFromGuest(AddOrderFromGuestDTO addOrder);
        void AddOrderWithoutCar(AddOrderWithoutCarDTO addOrder, int userID);
        void GarageAcceptOrder(int GFId, int userId);
        void GarageRejectOrder(int GFId, int userId);
        void GarageCancelOrder(int GFId, int userId);
        void GarageDoneOrder(int GFId, int userId);
        void UserCancelOrder(int userId, int GFId);
    }
}
