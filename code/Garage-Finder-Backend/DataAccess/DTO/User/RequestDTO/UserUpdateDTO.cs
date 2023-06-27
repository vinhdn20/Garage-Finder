namespace DataAccess.DTO.User.RequestDTO
{
    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public string AddressDetail { get; set; }
    }
}
