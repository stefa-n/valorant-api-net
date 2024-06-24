using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Valorant;

public static class Local
{
    public static async Task<string> GetToken()
    {
        var port = Lockfile.GetPort();
        var password = Lockfile.GetPassword();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"riot:{password}")));

            var response = await client.GetAsync($"https://127.0.0.1:{port}/entitlements/v1/token");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dataObj = JObject.Parse(responseString);

            return dataObj["accessToken"]?.ToString();
        }
    }

    public static async Task<string> GetEntitlement()
    {
        var port = Lockfile.GetPort();
        var password = Lockfile.GetPassword();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"riot:{password}")));

            var response = await client.GetAsync($"https://127.0.0.1:{port}/entitlements/v1/token");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dataObj = JObject.Parse(responseString);

            return dataObj["token"]?.ToString();
        }
    }

    public static async Task<string> GetPlayerUUID()
    {
        var port = Lockfile.GetPort();
        var password = Lockfile.GetPassword();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"riot:{password}")));

            var response = await client.GetAsync($"https://127.0.0.1:{port}/entitlements/v1/token");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dataObj = JObject.Parse(responseString);

            return dataObj["subject"]?.ToString();
        }
    }

    private class SendFriendRequestBody
    {
        public string game_name { get; set; }
        public string game_tag { get; set; }
    }

    public static async Task SendFriendRequest(string name, string tag)
    {
        var port = Lockfile.GetPort();
        var password = Lockfile.GetPassword();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"riot:{password}")));

            var friendRequestBody = new SendFriendRequestBody
            {
                game_name = name,
                game_tag = tag
            };

            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(friendRequestBody);
            var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new Uri($"https://127.0.0.1:{port}/chat/v4/friendrequests"), content);
            response.EnsureSuccessStatusCode();
        }
    }

    public static async Task<string> GetClientVersion()
    {
        var port = Lockfile.GetPort();
        var password = Lockfile.GetPassword();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"riot:{password}")));

            var response = await client.GetAsync("https://valorant-api.com/v1/version");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dataObj = JObject.Parse(responseString);

            return dataObj["data"]["riotClientVersion"]?.ToString();
        }
    }

    public static string GetClientPlatform()
    {
        // fake platform that works!
        return
            "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9";
    }
}