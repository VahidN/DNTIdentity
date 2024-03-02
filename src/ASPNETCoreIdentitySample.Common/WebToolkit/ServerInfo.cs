namespace ASPNETCoreIdentitySample.Common.WebToolkit;

public static class ServerInfo
{
    private static readonly string[] BinSplitArray = new[] { "bin" };
    public static string GetAppDataFolderPath()
    {
        var appDataFolderPath = Path.Combine(GetWwwRootPath(), "App_Data");
        if (!Directory.Exists(appDataFolderPath))
        {
            Directory.CreateDirectory(appDataFolderPath);
        }

        return appDataFolderPath;
    }

    public static string GetWwwRootPath()
    {
        return Path.Combine(
            AppContext.BaseDirectory.Split(BinSplitArray, StringSplitOptions.RemoveEmptyEntries)[0],
            "wwwroot");
    }
}
