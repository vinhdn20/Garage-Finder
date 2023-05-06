namespace Garage_Finder_Backend.Services.AuthService
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int ExpirationInHours { get; set; }
    }
}
