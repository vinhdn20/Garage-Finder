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
