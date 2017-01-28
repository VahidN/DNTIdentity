using System;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.DataLayer.Context
{
    public static class DbContextExtensions
    {
        public static void RunScopedContext<T>(this IServiceProvider serviceProvider, Action<IUnitOfWork, T> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>())
                {
                    callback(context, serviceScope.ServiceProvider.GetRequiredService<T>());
                }
            }
        }

        public static void RunScopedContext(this IServiceProvider serviceProvider, Action<IUnitOfWork> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>())
                {
                    callback(context);
                }
            }
        }
    }
}