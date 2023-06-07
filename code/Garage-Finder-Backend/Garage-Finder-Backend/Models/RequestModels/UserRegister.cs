namespace Garage_Finder_Backend.Models.RequestModels
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}
