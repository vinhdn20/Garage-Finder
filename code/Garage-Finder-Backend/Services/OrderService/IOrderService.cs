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
    }
}
