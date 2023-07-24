using GFData.Data;
using GFData.Models.Entity;

namespace DataAccess.DAO
{
    public class FeedbackDAO
    {
        private static FeedbackDAO instance = null;
        private static readonly object iLock = new object();
        public FeedbackDAO()
        {

        }

        public static FeedbackDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new FeedbackDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Feedback> GetAll()
        {
            var feedbacks = new List<Feedback>();
            try
            {
                using (var context = new GFDbContext())
                {
                    feedbacks = context.Feedback.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return feedbacks;
        }
        public List<Feedback> GetByGarage(int id)
        {
            var feedbacks = new List<Feedback>();
            try
            {
                using (var context = new GFDbContext())
                {
                    feedbacks = (from feedback in context.Feedback
                                 join order in context.Orders on feedback.OrderID equals order.OrderID
                                 where order.GarageID ==  id
                                 select new Feedback()
                                 {
                                     OrderID = feedback.OrderID,
                                     Content = feedback.Content,
                                     DateTime = feedback.DateTime,
                                     FeedbackID = feedback.FeedbackID,
                                     Star = feedback.Star
                                 }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return feedbacks;
        }

        public List<Feedback> GetByUserId(int id)
        {
            var feedbacks = new List<Feedback>();
            try
            {
                using (var context = new GFDbContext())
                {
                    feedbacks = (from feedback in context.Feedback
                                 join order in context.Orders on feedback.OrderID equals order.OrderID
                                 join car in context.Car on order.CarID equals car.CarID
                                 where car.UserID == id
                                 select new Feedback()
                                 {
                                     OrderID = feedback.OrderID,
                                     Content = feedback.Content,
                                     DateTime = feedback.DateTime,
                                     FeedbackID = feedback.FeedbackID,
                                     Star = feedback.Star
                                 }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return feedbacks;
        }

        public void SaveFeedback(Feedback feedback)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Feedback.Add(feedback);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateFeedback(Feedback feedback)
        {
            try
            {
                using (var context = new GFDbContext())
                {
                    context.Entry<Feedback>(feedback).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
