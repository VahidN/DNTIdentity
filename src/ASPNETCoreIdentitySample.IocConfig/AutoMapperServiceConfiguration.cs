using AutoMapper;
using System.Reflection;

namespace ASPNETCoreIdentitySample.IocConfig
{
    public static class AutoMapperServiceConfiguration
    {
        public static void ConfigureBase() => Mapper.Initialize(cfg =>
        {
            cfg.AddProfiles(
                typeof(ViewModels.BaseForMapping).GetTypeInfo().Assembly);
        });
    }
}
