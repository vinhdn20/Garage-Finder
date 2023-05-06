namespace Garage_Finder_Backend.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpriresDate { get; set; }
    }
}
