using DataAccess.DTO.Token;
using Mailjet.Client.Resources;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Garage_Finder_Backend
{
    public static class Extension
    {
        public static TokenInfor GetTokenInfor(this ClaimsPrincipal User)
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<TokenInfor>(jsonUser);
            return user;
        }
    }
}
