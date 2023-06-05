namespace Garage_Finder_Backend.Models.RequestModels
{
    public class ForgotPassModel
    {
        public string phoneNumber { get; set; }
        public string verifyCode { get; set; }
        public string newPassword { get; set; }
    }
}
