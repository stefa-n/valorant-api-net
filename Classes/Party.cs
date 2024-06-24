using Newtonsoft.Json.Linq;

namespace Valorant;

public static class Party
{
    public static async Task StartQueue()
    {
        string region = Logfile.GetRegion();
        string shard = Logfile.GetShard();

        string clientPlatform = Local.GetClientPlatform();
        string clientVersion = await Local.GetClientVersion();
        string entitlementToken = await Local.GetEntitlement();
        string authorization = await Local.GetToken();

        string party = await GetPartyId(await Local.GetPlayerUUID());

        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (HttpClient client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            HttpResponseMessage response = await client.PostAsync(
                new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/parties/{party}/matchmaking/join"), null);
            response.EnsureSuccessStatusCode();
        }
    }

    public static async Task LeaveQueue()
    {
        string region = Logfile.GetRegion();
        string shard = Logfile.GetShard();

        string clientPlatform = Local.GetClientPlatform();
        string clientVersion = await Local.GetClientVersion();
        string entitlementToken = await Local.GetEntitlement();
        string authorization = await Local.GetToken();

        string party = await GetPartyId(await Local.GetPlayerUUID());

        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (HttpClient client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            HttpResponseMessage response = await client.PostAsync(
                new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/parties/{party}/matchmaking/leave"),
                null);
            response.EnsureSuccessStatusCode();
        }
    }

    public static async Task Invite(string name, string tag)
    {
        string region = Logfile.GetRegion();
        string shard = Logfile.GetShard();

        string clientPlatform = Local.GetClientPlatform();
        string clientVersion = await Local.GetClientVersion();
        string entitlementToken = await Local.GetEntitlement();
        string authorization = await Local.GetToken();

        string party = await GetPartyId(await Local.GetPlayerUUID());

        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (HttpClient client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            HttpResponseMessage response = await client.PostAsync(
                new Uri(
                    $"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/parties/{party}/invites/name/{name}/tag/{tag}"),
                null);
            response.EnsureSuccessStatusCode();
        }
    }

    public static async Task<string> GetPartyId(string puuid)
    {
        string region = Logfile.GetRegion();
        string shard = Logfile.GetShard();

        string clientPlatform = Local.GetClientPlatform();
        string clientVersion = await Local.GetClientVersion();

        string entitlementToken = await Local.GetEntitlement();
        string authorization = await Local.GetToken();

        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (HttpClient client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            HttpResponseMessage response =
                await client.GetAsync($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/players/{puuid}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dataObj = JObject.Parse(responseString);

            return dataObj["CurrentPartyID"]?.ToString();
        }
    }
}