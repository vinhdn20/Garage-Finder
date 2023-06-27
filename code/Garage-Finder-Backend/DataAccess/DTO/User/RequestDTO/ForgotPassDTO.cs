namespace DataAccess.DTO.User.RequestDTO
{
    public class ForgotPassDTO
    {
        public string phoneNumber { get; set; }
        public string verifyCode { get; set; }
        public string newPassword { get; set; }
    }
}
