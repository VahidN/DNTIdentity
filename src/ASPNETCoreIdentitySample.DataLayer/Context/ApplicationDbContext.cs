using ASPNETCoreIdentitySample.Common.EFCoreToolkit;
using ASPNETCoreIdentitySample.DataLayer.Configurations;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.DataLayer.Context;

/// <summary>
///     More info: http://www.dntips.ir/post/2577
///     and http://www.dntips.ir/post/2578
///     plus http://www.dntips.ir/post/2491
/// </summary>
public class ApplicationDbContext(DbContextOptions options)
    : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options), IUnitOfWork
{
    // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`

    public virtual DbSet<Category> Categories { set; get; }

    public virtual DbSet<Product> Products { set; get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // it should be placed here, otherwise it will rewrite the following settings!
        base.OnModelCreating(builder);

        // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
        // Adds all of the ASP.NET Core Identity related mappings at once.
        builder.AddCustomIdentityMappings();

        // Custom application mappings
        builder.SetDecimalPrecision();
        builder.AddDateTimeUtcKindConverter();

        // This should be placed here, at the end.
        builder.AddAuditableShadowProperties();
    }

    #region BaseClass

    public virtual DbSet<AppLogItem> AppLogItems { get; set; }

    public virtual DbSet<AppSqlCache> AppSqlCache { get; set; }

    public virtual DbSet<AppDataProtectionKey> AppDataProtectionKeys { get; set; }

    public void AddRange<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
        => Set<TEntity>().AddRange(entities);

    public async Task ExecuteTransactionAsync(Func<Task> action)
    {
        // https://www.dntips.ir/post/3247
        var strategy = Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await Database.BeginTransactionAsync();
            await action();
            await transaction.CommitAsync();
        });
    }

    public void ExecuteSqlInterpolatedCommand(FormattableString query) => Database.ExecuteSqlInterpolated(query);

    public void ExecuteSqlRawCommand(string query, params object[] parameters)
        => Database.ExecuteSqlRaw(query, parameters);

    public T GetShadowPropertyValue<T>(object entity, string propertyName)
        where T : IConvertible
    {
        var value = Entry(entity).Property(propertyName).CurrentValue;

        return value != null ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default;
    }

    public object GetShadowPropertyValue(object entity, string propertyName)
        => Entry(entity).Property(propertyName).CurrentValue;

    public void MarkAsChanged<TEntity>(TEntity entity)
        where TEntity : class
        => Update(entity);

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
        => Set<TEntity>().RemoveRange(entities);

    #endregion
}