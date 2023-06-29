using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class GuestOrderDAO
    {
        private static GuestOrderDAO instance = null;
        private static readonly object iLock = new object();
        public GuestOrderDAO()
        {

        }

        public static GuestOrderDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new GuestOrderDAO();
                    }
                    return instance;
                }
            }
        }
        public List<GuestOrder> GetList()
        {
            var listOrders = new List<GuestOrder>();
            try
            {
                using (var context = new GFDbContext())
                {
                    //listOrders = context.Orders.ToList();
                    listOrders = (from order in context.GuestOrder
                                  select new GuestOrder
                                  {
                                      GuestOrderID = order.GuestOrderID,
                                      GFOrderID = order.GFOrderID,
                                      GarageID = order.GarageID,
                                      CategoryGarageID = order.CategoryGarageID,
                                      TimeCreate = order.TimeCreate,
                                      TimeUpdate = order.TimeUpdate,
                                      TimeAppointment = order.TimeAppointment,
                                      Status = order.Status,
                                      Content = order.Content,
                                      ImageOrders = context.ImageGuestOrders.Where(x => x.GuestOrderID == order.GuestOrderID).ToList(),
                                      FileOrders = context.FileGuestOrders.Where(x => x.GuestOrderID == order.GuestOrderID).ToList(),
                                  }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }
        
        public List<GuestOrder> GetByGarageId(int id)
        {
            List<GuestOrder> order = null;
            try
            {
                var db = new GFDbContext();
                order = db.GuestOrder.Where(c => c.GarageID == id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }

        public GuestOrder GetByGFId(int id)
        {
            GuestOrder order = null;
            try
            {
                var db = new GFDbContext();
                order = db.GuestOrder.SingleOrDefault(c => c.GFOrderID == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }
        public GuestOrder GetById(int id)
        {
            GuestOrder order = null;
            try
            {
                var db = new GFDbContext();
                order = db.GuestOrder.SingleOrDefault(c => c.GuestOrderID == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }

        public void Add(GuestOrder order)
        {
            try
            {
                var db = new GFDbContext();
                int id = GetGFOrderId() + 1;
                order.GFOrderID = id;
                db.GuestOrder.Add(order);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(GuestOrder order)
        {
            try
            {
                GuestOrder _order = GetById(order.GuestOrderID);
                if (_order != null)
                {
                    var db = new GFDbContext();
                    //db.Entry<Order>(order).State = EntityState.Modified;
                    db.GuestOrder.Update(order);
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
                GuestOrder _order = GetById(id);
                if (_order != null)
                {
                    var db = new GFDbContext();
                    db.GuestOrder.Remove(_order);
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

        public int GetGFOrderId()
        {
            int id = -1;
            try
            {
                var db = new GFDbContext();
                if (db.Orders.Count() != 0)
                {
                    id = db.Orders.Max(x => x.GFOrderID);
                }
                if (db.GuestOrder.Count() != 0)
                {
                    int gid = db.GuestOrder.Max(x => x.GFOrderID);
                    if (id < gid)
                    {
                        id = gid;
                    }
                }
                return id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

