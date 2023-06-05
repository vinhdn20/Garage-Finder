using Newtonsoft.Json;
using RestSharp;

namespace Services.GgService
{
    public class GoogleService
    {
        public static async Task<dynamic> GetUserInforByAccessTokenAsync(string accessToken)
        {
            var client = new RestClient();
            var request = new RestRequest($"https://www.googleapis.com/oauth2/v3/userinfo?access_token={accessToken}");
            RestResponse response = await client.ExecuteAsync(request);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            return obj;
        }
    }
}
