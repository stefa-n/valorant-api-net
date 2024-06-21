using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Valorant
{
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
                
                HttpResponseMessage response = await client.PostAsync(new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/parties/{party}/matchmaking/join"), null);
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
                
                HttpResponseMessage response = await client.PostAsync(new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/parties/{party}/matchmaking/leave"), null);
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
                
                HttpResponseMessage response = await client.PostAsync(new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/parties/{party}/invites/name/{name}/tag/{tag}"), null);
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
                
                HttpResponseMessage response = await client.GetAsync($"https://glz-{region}-1.{shard}.a.pvp.net/parties/v1/players/{puuid}");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var dataObj = JObject.Parse(responseString);
                
                return dataObj["CurrentPartyID"]?.ToString();
            }
        }
    }
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
                
                var response = await client.PostAsync(new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/pregame/v1/matches/{match}/select/{character}"), null);
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
                
                var response = await client.PostAsync(new Uri($"https://glz-{region}-1.{shard}.a.pvp.net/pregame/v1/matches/{match}/lock/{character}"), null);
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
                
                var response = await client.GetAsync($"https://glz-{region}-1.{shard}.a.pvp.net/pregame/v1/players/{puuid}");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var dataObj = JObject.Parse(responseString);
                
                return dataObj["MatchID"]?.ToString();
            }
        }
    }
    public static class Local
    {
        static async Task Main()
        {
            try
            {
                Console.WriteLine($"Token: {await GetToken()}");
                Console.WriteLine($"Entitlements Token: {await GetEntitlement()}");
                Console.WriteLine($"Client Version: {await GetClientVersion()}");
                Console.WriteLine($"Client Platform: {GetClientPlatform()}");
                Console.WriteLine($"Party ID: {await Party.GetPartyId(await Local.GetPlayerUUID())}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
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
        { // fake platform that works!
            return
                "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9";
        }
    }
    public static class Logfile
    {
        static string logFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
            "VALORANT", "Saved", "Logs", "ShooterGame.log");

        public static string GetRegion()
        {
            try
            {
                using (var fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var logContent = reader.ReadToEnd();
                        var regex = new Regex(@"https://glz-(.+?)-1\.(.+?)\.a\.pvp\.net");
                        var match = regex.Match(logContent);

                        if (match.Success)
                        {
                            return match.Groups[1].Value;
                        }
                        else
                        {
                            Console.WriteLine("Region not found in log file.");
                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }

        public static string GetShard()
        {
            try
            {
                using (var fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var logContent = reader.ReadToEnd();
                        var regex = new Regex(@"https://glz-(.+?)-1\.(.+?)\.a\.pvp\.net");
                        var match = regex.Match(logContent);

                        if (match.Success)
                        {
                            return match.Groups[2].Value;
                        }
                        else
                        {
                            Console.WriteLine("Shard not found in log file.");
                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }
    }
    public static class Lockfile
    {
        private static string _lockfilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Riot Games", "Riot Client", "Config", "lockfile");

        private static string[] ReadLockfile()
        {
            try
            {
                using (var fileStream = new FileStream(_lockfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        var lockfileContent = reader.ReadToEnd();
                        return lockfileContent.Split(':');
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new string[0];
            }
        }

        public static string GetName()
        {
            var lockfileParts = ReadLockfile();
            return lockfileParts.Length >= 1 ? lockfileParts[0] : string.Empty;
        }

        public static int GetPid()
        {
            var lockfileParts = ReadLockfile();
            return lockfileParts.Length >= 2 ? int.Parse(lockfileParts[1]) : 0;
        }

        public static int GetPort()
        {
            var lockfileParts = ReadLockfile();
            return lockfileParts.Length >= 3 ? int.Parse(lockfileParts[2]) : 0;
        }

        public static string GetPassword()
        {
            var lockfileParts = ReadLockfile();
            return lockfileParts.Length >= 4 ? lockfileParts[3] : string.Empty;
        }

        public static string GetProtocol()
        {
            var lockfileParts = ReadLockfile();
            return lockfileParts.Length >= 5 ? lockfileParts[4] : string.Empty;
        }
    }
    public static class Agents
    {
        public static string Gekko = "e370fa57-4757-3604-3648-499e1f642d3f";
        public static string Fade = "dade69b4-4f5a-8528-247b-219e5a1facd6";
        public static string Breach = "5f8d3a7f-467b-97f3-062c-13acf203c006";
        public static string Deadlock = "cc8b64c8-4b25-4ff9-6e7f-37b4da43d235";
        public static string Raze = "f94c3b30-42be-e959-889c-5aa313dba261";
        public static string Chamber = "22697a3d-45bf-8dd7-4fec-84a9e28c69d7";
        public static string KAYO = "601dbbe7-43ce-be57-2a40-4abd24953621";
        public static string Skye = "6f2a04ca-43e0-be17-7f36-b3908627744d";
        public static string Cypher = "117ed9e3-49f3-6512-3ccf-0cada7e3823b";
        public static string Sova = "320b2a48-4d9b-a075-30f1-1f93a9b638fa";
        public static string Killjoy = "1e58de9c-4950-5125-93e9-a0aee9f98746";
        public static string Harbor = "95b78ed7-4637-86d9-7e41-71ba8c293152";
        public static string Viper = "707eab51-4836-f488-046a-cda6bf494859";
        public static string Phoenix = "eb93336a-449b-9c1b-0a54-a891f7921d69";
        public static string Astra = "41fb69c1-4189-7b37-f117-bcaf1e96f1bf";
        public static string Brimstone = "9f0d8ba9-4140-b941-57d3-a7ad57c6b417";
        public static string Iso = "0e38b510-41a8-5780-5e8f-568b2a4f2d6c";
        public static string Clove = "1dbf2edd-4729-0984-3115-daa5eed44993";
        public static string Neon = "bb2a4828-46eb-8cd1-e765-15848195d751";
        public static string Yoru = "7f94d92c-4234-0a36-9646-3a87eb8b5c89";
        public static string Sage = "569fdd95-4d10-43ab-ca70-79becc718b46";
        public static string Reyna = "a3bfb853-43b2-7238-a4f1-ad90e9e46bcc";
        public static string Omen = "8e253930-4c05-31dd-1b6c-968525494517";
        public static string Jett = "add6443a-41bd-e414-f6ad-e58d267f4e95";
    }
}
