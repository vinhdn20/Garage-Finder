using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class OrdersDAO
    {
        private static OrdersDAO instance = null;
        private static readonly object iLock = new object();
        public OrdersDAO()
        {

        }

        public static OrdersDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new OrdersDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Orders> GetList()
        {
            var listOrders = new List<Orders>();
            try
            {
                using (var context = new GFDbContext())
                {
                    //listOrders = context.Orders.ToList();
                    listOrders = (from order in context.Orders
                                  select new Orders
                                  {
                                      OrderID = order.OrderID,
                                      CarID = order.CarID,
                                      GarageID = order.GarageID,
                                      //ServiceID = order.ServiceID,
                                      TimeCreate = order.TimeCreate,
                                      TimeUpdate = order.TimeUpdate,
                                      TimeAppointment = order.TimeAppointment,
                                      Status = order.Status,
                                      Content = order.Content,
                                      ImageOrders = context.ImageOrders.Where(x => x.OrderID == order.OrderID).ToList(),
                                      FileOrders = context.FileOrders.Where(x => x.OrderID == order.OrderID).ToList(),
                                  }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public List<Orders> GetListByUserID(int id)
        {
            var listOrders = new List<Orders>();
            try
            {

                using (var context = new GFDbContext())
                {
                    listOrders = (from order in context.Orders
                                  join car in context.Car on order.CarID equals car.CarID
                                  join user in context.User on car.UserID equals user.UserID
                                  where user.UserID == id 
                                  select new Orders 
                                  {
                                      OrderID = order.OrderID,
                                      CarID = order.CarID,
                                      GarageID = order.GarageID,
                                      //ServiceID = order.ServiceID,
                                      TimeCreate = order.TimeCreate,
                                      TimeUpdate = order.TimeUpdate,
                                      TimeAppointment = order.TimeAppointment,
                                      Status = order.Status,
                                      Content = order.Content,
                                      ImageOrders = context.ImageOrders.Where(x => x.OrderID == order.OrderID).ToList(),
                                      FileOrders = context.FileOrders.Where(x => x.OrderID == order.OrderID).ToList(),
                                  } ).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }


        public Orders GetById(int id)
        {
            Orders order = null;
            try
            {
                var db = new GFDbContext();
                order = db.Orders.SingleOrDefault(c => c.OrderID == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }

        public void Add(Orders order)
        {
            try
            {
                var db = new GFDbContext();
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Orders order)
        {
            try
            {
                Orders _order = GetById(order.OrderID);
                if (_order != null)
                {
                    var db = new GFDbContext();
                    //db.Entry<Order>(order).State = EntityState.Modified;
                    db.Orders.Update(order);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Cannot find Order");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Orders _order = GetById(id);
                if (_order != null)
                {
                    var db = new GFDbContext();
                    db.Orders.Remove(_order);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not exist!!!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
