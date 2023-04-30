namespace Garage_Finder_Backend.Models.Entity
{
    public class Feedback
    {
        public int CommentID { get; set; }  
        public int GarageID { get; set; }   
        public int UserID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }

    }
}
