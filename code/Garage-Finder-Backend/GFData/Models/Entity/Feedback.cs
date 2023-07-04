using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFData.Models.Entity
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }
        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Orders Orders { get; set;}

    }
}
