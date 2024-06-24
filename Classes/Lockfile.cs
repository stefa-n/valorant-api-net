namespace Valorant;

public static class Lockfile
{
    private static string _lockfilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Riot Games",
            "Riot Client", "Config", "lockfile");

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