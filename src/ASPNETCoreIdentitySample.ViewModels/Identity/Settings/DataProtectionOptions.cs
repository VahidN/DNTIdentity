using System;

namespace ASPNETCoreIdentitySample.ViewModels.Identity.Settings
{
    public class DataProtectionOptions
    {
        public TimeSpan DataProtectionKeyLifetime { get; set; }
        public string ApplicationName { get; set; }
    }
}