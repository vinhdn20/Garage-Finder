using System.ComponentModel.DataAnnotations;

namespace GFData.Models.Entity
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }  
        public int GarageID { get; set; }   
        public int UserID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }

    }
}
