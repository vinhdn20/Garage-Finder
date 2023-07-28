using DataAccess.DTO.Token;
using Mailjet.Client.Resources;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Garage_Finder_Backend
{
    public static class Extension
    {

        public static string GetIp(this HttpContext httpContext)
        {
            string ip = httpContext.GetServerVariable("REMOTE_HOST");
            if (ip == null)
            {
                ip = httpContext.GetServerVariable("REMOTE_ADDR");
            }
            return ip;
        }
    }
}
