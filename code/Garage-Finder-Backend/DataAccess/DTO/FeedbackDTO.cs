namespace DataAccess.DTO
{
    public class FeedbackDTO
    {
        public int CommentID { get; set; }  
        public int GarageID { get; set; }   
        public int UserID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }

    }
}
