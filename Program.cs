namespace Valorant
{
    class Test
    {
        private static async Task Main()
        {
            try
            {
                Console.WriteLine($"Token: {await Local.GetToken()}");
                Console.WriteLine($"Entitlements Token: {await Local.GetEntitlement()}");
                Console.WriteLine($"Client Version: {await Local.GetClientVersion()}");
                Console.WriteLine($"Client Platform: {Local.GetClientPlatform()}");
                Console.WriteLine($"Party ID: {await Party.GetPartyId(await Local.GetPlayerUUID())}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}