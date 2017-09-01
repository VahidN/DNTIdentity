using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Common.PersianToolkit;
using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ASPNETCoreIdentitySample.DataLayer.Context
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// plus http://www.dotnettips.info/post/2491
    /// </summary>
    public abstract class ApplicationDbContextBase :
        IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IUnitOfWork
    {
        protected readonly IHostingEnvironment HostingEnvironment;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected readonly ILogger<ApplicationDbContextBase> Logger;
        protected readonly IOptionsSnapshot<SiteSettings> SiteSettings;
        protected ApplicationDbContextBase(
            IOptionsSnapshot<SiteSettings> siteSettings,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            ILogger<ApplicationDbContextBase> logger)
        {
            SiteSettings = siteSettings;
            SiteSettings.CheckArgumentIsNull(nameof(SiteSettings));

            HttpContextAccessor = httpContextAccessor;
            HttpContextAccessor.CheckArgumentIsNull(nameof(HttpContextAccessor));

            HostingEnvironment = hostingEnvironment;
            HostingEnvironment.CheckArgumentIsNull(nameof(HostingEnvironment));

            Logger = logger;
            Logger.CheckArgumentIsNull(nameof(Logger));
        }

        public virtual DbSet<AppLogItem> AppLogItems { get; set; }
        public virtual DbSet<AppSqlCache> AppSqlCache { get; set; }
        public virtual DbSet<AppDataProtectionKey> AppDataProtectionKeys { get; set; }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void ExecuteSqlCommand(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = this.Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return this.Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        protected void BeforeSaveTriggers()
        {
            ValidateEntities();
            SetShadowProperties();
            this.ApplyCorrectYeKe();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var siteSettingsValue = SiteSettings.Value;
            siteSettingsValue.CheckArgumentIsNull(nameof(siteSettingsValue));
            var connectionString = siteSettingsValue.GetDbConnectionString(HostingEnvironment.WebRootPath);

            switch (siteSettingsValue.ActiveDatabase)
            {
                case ActiveDatabase.InMemoryDatabase:
                    optionsBuilder.UseInMemoryDatabase(siteSettingsValue.ConnectionStrings.LocalDb.InitialCatalog);
                    break;

                case ActiveDatabase.LocalDb:
                case ActiveDatabase.SqlServer:
                    optionsBuilder.UseSqlServer(
                        connectionString
                        , serverDbContextOptionsBuilder =>
                        {
                            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                            serverDbContextOptionsBuilder.CommandTimeout(minutes);
                            serverDbContextOptionsBuilder.EnableRetryOnFailure();
                        });
                    break;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
            }

            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Log(CoreEventId.IncludeIgnoredWarning);
            });
        }

        protected void SetShadowProperties()
        {
            ChangeTracker.SetAuditableEntityPropertyValues(HttpContextAccessor);
        }

        protected void ValidateEntities()
        {
            var errors = this.GetValidationErrors();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                Logger.LogError(errors);
                throw new InvalidOperationException(errors);
            }
        }
    }
}