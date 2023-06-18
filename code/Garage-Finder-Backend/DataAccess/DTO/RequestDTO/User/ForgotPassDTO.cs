namespace DataAccess.DTO.RequestDTO.User
{
    public class ForgotPassDTO
    {
        public string phoneNumber { get; set; }
        public string verifyCode { get; set; }
        public string newPassword { get; set; }
    }
}
