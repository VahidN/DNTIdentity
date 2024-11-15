namespace ASPNETCoreIdentitySample.Common.WebToolkit;

public static class ServerInfo
{
    private static readonly string[] BinSplitArray = ["bin"];

    public static string GetAppDataFolderPath()
    {
        var appDataFolderPath = Path.Combine(GetWwwRootPath(), path2: "App_Data");

        if (!Directory.Exists(appDataFolderPath))
        {
            Directory.CreateDirectory(appDataFolderPath);
        }

        return appDataFolderPath;
    }

    public static string GetWwwRootPath()
        => Path.Combine(AppContext.BaseDirectory.Split(BinSplitArray, StringSplitOptions.RemoveEmptyEntries)[0],
            path2: "wwwroot");
}