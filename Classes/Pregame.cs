using Newtonsoft.Json.Linq;

namespace Valorant;

public static class Pregame
{
    public static async Task SelectCharacter(string character)
    {
        var region = Logfile.GetRegion();
        var shard = Logfile.GetShard();

        var clientPlatform = Local.GetClientPlatform();
        var clientVersion = await Local.GetClientVersion();
        var entitlementToken = await Local.GetEntitlement();
        var authorization = await Local.GetToken();

        var match = await Pregame.GetMatchId(await Local.GetPlayerUUID());

        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            var response =
                await client.PostAsync(
                    new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/pregame/v1/matches/{match}/select/{character}"),
                    null);
            response.EnsureSuccessStatusCode();
        }
    }

    public static async Task LockCharacter(string character)
    {
        var region = Logfile.GetRegion();
        var shard = Logfile.GetShard();

        var clientPlatform = Local.GetClientPlatform();
        var clientVersion = await Local.GetClientVersion();
        var entitlementToken = await Local.GetEntitlement();
        var authorization = await Local.GetToken();

        var match = await Pregame.GetMatchId(await Local.GetPlayerUUID());

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            var response =
                await client.PostAsync(
                    new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/pregame/v1/matches/{match}/lock/{character}"),
                    null);
            response.EnsureSuccessStatusCode();
        }
    }

    public static async Task<string> GetMatchId(string puuid)
    {
        var region = Logfile.GetRegion();
        var shard = Logfile.GetShard();

        var clientPlatform = Local.GetClientPlatform();
        var clientVersion = await Local.GetClientVersion();
        var entitlementToken = await Local.GetEntitlement();
        var authorization = await Local.GetToken();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };

        using (var client = new HttpClient(handler))
        {
            client.DefaultRequestHeaders.Add("X-Riot-ClientPlatform", clientPlatform);
            client.DefaultRequestHeaders.Add("X-Riot-ClientVersion", clientVersion);
            client.DefaultRequestHeaders.Add("X-Riot-Entitlements-JWT", entitlementToken);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

            var response =
                await client.GetAsync($"https://glz-{region}-1.{shard}.a.pvp.net/pregame/v1/players/{puuid}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dataObj = JObject.Parse(responseString);

            return dataObj["MatchID"]?.ToString();
        }
    }
}