using GFData.Data;
using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object iLock = new object();
        public OrderDetailDAO()
        {

        }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetList()
        {
            List<OrderDetail> orderDetails;
            try
            {
                var db = new GFDbContext();
                orderDetails = db.OrderDetails.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return orderDetails;
        }

        public OrderDetail GetById(int? id)
        {
            OrderDetail orderDetail = null;
            try
            {
                var db = new GFDbContext();
                orderDetail = db.OrderDetails.Include(c => c.Service).SingleOrDefault(c => c.OrderID == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return orderDetail;
        }

        public void Add(OrderDetail orderDetail)
        {
            try
            {
                var db = new GFDbContext();
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = GetById((int)orderDetail.OrderID);
                if (_orderDetail != null)
                {
                    var db = new GFDbContext();
                    //db.Entry<OrderDetail>(orderDetail).State = EntityState.Modified;
                    db.OrderDetails.Update(orderDetail);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Cannot find Order detail");
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
                OrderDetail _orderDetail = GetById(id);
                if (_orderDetail != null)
                {
                    var db = new GFDbContext();
                    db.OrderDetails.Remove(_orderDetail);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Order detail does not exist!!!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
