using System;
using System.IO;
using System.Linq;
using ASPNETCoreIdentitySample.Common.GuardToolkit;

namespace ASPNETCoreIdentitySample.ViewModels.Identity.Settings
{
    public static class SiteSettingsExtesnsions
    {
        public static string GetDbConnectionString(this SiteSettings siteSettingsValue, string webRootPath)
        {
            siteSettingsValue.CheckArgumentIsNull(nameof(siteSettingsValue));

            switch (siteSettingsValue.ActiveDatabase)
            {
                case ActiveDatabase.InMemoryDatabase:
                    return null;

                case ActiveDatabase.LocalDb:
                    var attachDbFilename = siteSettingsValue.ConnectionStrings.LocalDb.AttachDbFilename;
                    if (string.IsNullOrWhiteSpace(webRootPath))
                    {
                        webRootPath = Path.Combine(AppContext.BaseDirectory.Split(new[] { "bin" }, StringSplitOptions.RemoveEmptyEntries).First(), "wwwroot");
                    }
                    var attachDbFilenamePath = Path.Combine(webRootPath, "App_Data", attachDbFilename);
                    var localDbInitialCatalog = siteSettingsValue.ConnectionStrings.LocalDb.InitialCatalog;
                    return $@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog={localDbInitialCatalog};AttachDbFilename={attachDbFilenamePath};Integrated Security=True;MultipleActiveResultSets=True;";

                case ActiveDatabase.SqlServer:
                    return siteSettingsValue.ConnectionStrings.SqlServer.ApplicationDbContextConnection;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
            }
        }
    }
}