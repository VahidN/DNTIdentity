using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ASPNETCoreIdentitySample.Entities.Identity;
using DNTCommon.Web.Core;

namespace ASPNETCoreIdentitySample.Entities.AuditableEntity
{
    public class AppShadowProperties
    {
        public string UserAgent { set; get; }
        public string UserIp { set; get; }
        public DateTime Now { set; get; }
        public int? UserId { set; get; }
    }

    /// <summary>
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// and http://www.dotnettips.info/post/2507
    /// and http://www.dotnettips.info/post/2232
    /// </summary>
    public static class AuditableShadowProperties
    {
        public static readonly Func<object, string> EFPropertyCreatedByBrowserName =
                                        entity => EF.Property<string>(entity, CreatedByBrowserName);
        public static readonly string CreatedByBrowserName = nameof(CreatedByBrowserName);

        public static readonly Func<object, string> EFPropertyModifiedByBrowserName =
                                        entity => EF.Property<string>(entity, ModifiedByBrowserName);
        public static readonly string ModifiedByBrowserName = nameof(ModifiedByBrowserName);

        public static readonly Func<object, string> EFPropertyCreatedByIp =
                                        entity => EF.Property<string>(entity, CreatedByIp);
        public static readonly string CreatedByIp = nameof(CreatedByIp);

        public static readonly Func<object, string> EFPropertyModifiedByIp =
                                        entity => EF.Property<string>(entity, ModifiedByIp);
        public static readonly string ModifiedByIp = nameof(ModifiedByIp);

        public static readonly Func<object, int?> EFPropertyCreatedByUserId =
                                        entity => EF.Property<int?>(entity, CreatedByUserId);
        public static readonly string CreatedByUserId = nameof(CreatedByUserId);

        public static readonly Func<object, int?> EFPropertyModifiedByUserId =
                                        entity => EF.Property<int?>(entity, ModifiedByUserId);
        public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);

        public static readonly Func<object, DateTime?> EFPropertyCreatedDateTime =
                                        entity => EF.Property<DateTime?>(entity, CreatedDateTime);
        public static readonly string CreatedDateTime = nameof(CreatedDateTime);

        public static readonly Func<object, DateTime?> EFPropertyModifiedDateTime =
                                        entity => EF.Property<DateTime?>(entity, ModifiedDateTime);
        public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

        public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                                .GetEntityTypes()
                                                .Where(e => typeof(IAuditableEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(CreatedByBrowserName).HasMaxLength(1000);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(ModifiedByBrowserName).HasMaxLength(1000);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(CreatedByIp).HasMaxLength(255);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(ModifiedByIp).HasMaxLength(255);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<int?>(CreatedByUserId);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<int?>(ModifiedByUserId);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime?>(CreatedDateTime);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime?>(ModifiedDateTime);
            }
        }

        /// <summary>
        /// More info: http://www.dotnettips.info/post/2507
        /// </summary>
        public static void SetAuditableEntityPropertyValues(
            this ChangeTracker changeTracker,
            AppShadowProperties props)
        {
            if (props == null)
            {
                return;
            }

            var modifiedEntries = changeTracker.Entries<IAuditableEntity>()
                                            .Where(x => x.State == EntityState.Modified);
            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.SetModifiedShadowProperties(props);
            }

            var addedEntries = changeTracker.Entries<IAuditableEntity>()
                                            .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.SetAddedShadowProperties(props);
            }
        }

        public static void SetAddedShadowProperties(this EntityEntry<IAuditableEntity> addedEntry, AppShadowProperties props)
        {
            if (props == null)
            {
                return;
            }

            addedEntry.Property(CreatedDateTime).CurrentValue = props.Now;
            if (!string.IsNullOrWhiteSpace(props.UserAgent)) addedEntry.Property(CreatedByBrowserName).CurrentValue = props.UserAgent;
            if (!string.IsNullOrWhiteSpace(props.UserIp)) addedEntry.Property(CreatedByIp).CurrentValue = props.UserIp;
            if (props.UserId.HasValue) addedEntry.Property(CreatedByUserId).CurrentValue = props.UserId;
        }

        public static void SetAddedShadowProperties(this EntityEntry<AppLogItem> addedEntry, AppShadowProperties props)
        {
            if (props == null)
            {
                return;
            }

            addedEntry.Property(CreatedDateTime).CurrentValue = props.Now;
            if (!string.IsNullOrWhiteSpace(props.UserAgent)) addedEntry.Property(CreatedByBrowserName).CurrentValue = props.UserAgent;
            if (!string.IsNullOrWhiteSpace(props.UserIp)) addedEntry.Property(CreatedByIp).CurrentValue = props.UserIp;
            if (props.UserId.HasValue) addedEntry.Property(CreatedByUserId).CurrentValue = props.UserId;
        }

        public static void SetModifiedShadowProperties(this EntityEntry<IAuditableEntity> modifiedEntry, AppShadowProperties props)
        {
            if (props == null)
            {
                return;
            }

            modifiedEntry.Property(ModifiedDateTime).CurrentValue = props.Now;
            if (!string.IsNullOrWhiteSpace(props.UserAgent)) modifiedEntry.Property(ModifiedByBrowserName).CurrentValue = props.UserAgent;
            if (!string.IsNullOrWhiteSpace(props.UserIp)) modifiedEntry.Property(ModifiedByIp).CurrentValue = props.UserIp;
            if (props.UserId.HasValue) modifiedEntry.Property(ModifiedByUserId).CurrentValue = props.UserId;
        }

        public static AppShadowProperties GetShadowProperties(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                return null;
            }

            var httpContext = httpContextAccessor?.HttpContext;
            return new AppShadowProperties
            {
                UserAgent = httpContext?.Request?.Headers["User-Agent"].ToString(),
                UserIp = httpContext?.Connection?.RemoteIpAddress?.ToString(),
                Now = DateTime.UtcNow,
                UserId = getUserId(httpContext)
            };
        }

        private static int? getUserId(HttpContext httpContext)
        {
            int? userId = null;
            var userIdValue = httpContext?.User?.Identity?.GetUserId();
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = int.Parse(userIdValue);
            }
            return userId;
        }
    }
}