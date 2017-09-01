using System;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.DataLayer.Context
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2510
    /// </summary>
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

        public static T RunScopedContext<T>(this IServiceProvider serviceProvider, Func<IUnitOfWork, T> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>())
                {
                    return callback(context);
                }
            }
        }
    }
}