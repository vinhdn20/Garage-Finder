namespace Garage_Finder_Backend.Models.RequestModels
{
    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string LinkImage { get; set; }
    }
}
