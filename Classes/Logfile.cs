using System.Text.RegularExpressions;

namespace Valorant;

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