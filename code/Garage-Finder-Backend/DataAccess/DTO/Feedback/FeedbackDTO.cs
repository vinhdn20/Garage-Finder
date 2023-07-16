using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DTO.Feedback
{
    public class FeedbackDTO
    {
        public int FeedbackID { get; set; }
        public string Name { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string LinkImage { get; set; }

    }
}
