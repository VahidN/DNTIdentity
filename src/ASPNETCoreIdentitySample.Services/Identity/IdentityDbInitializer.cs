using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2577
///     And http://www.dntips.ir/post/2578
/// </summary>
public class IdentityDbInitializer : IIdentityDbInitializer
{
    private readonly IOptionsSnapshot<SiteSettings> _adminUserSeedOptions;
    private readonly IApplicationUserManager _applicationUserManager;
    private readonly ILogger<IdentityDbInitializer> _logger;
    private readonly IApplicationRoleManager _roleManager;
    private readonly IServiceScopeFactory _scopeFactory;

    public IdentityDbInitializer(
        IApplicationUserManager applicationUserManager,
        IServiceScopeFactory scopeFactory,
        IApplicationRoleManager roleManager,
        IOptionsSnapshot<SiteSettings> adminUserSeedOptions,
        ILogger<IdentityDbInitializer> logger
    )
    {
        _applicationUserManager = applicationUserManager;
        ArgumentNullException.ThrowIfNull(applicationUserManager);

        _scopeFactory = scopeFactory;
        ArgumentNullException.ThrowIfNull(scopeFactory);

        _roleManager = roleManager;
        ArgumentNullException.ThrowIfNull(roleManager);

        _adminUserSeedOptions = adminUserSeedOptions;
        ArgumentNullException.ThrowIfNull(adminUserSeedOptions);

        _logger = logger;
        ArgumentNullException.ThrowIfNull(logger);
    }

    /// <summary>
    ///     Applies any pending migrations for the context to the database.
    ///     Will create the database if it does not already exist.
    /// </summary>
    public void Initialize()
    {
        _scopeFactory.RunScopedService<ApplicationDbContext>(context =>
        {
            if (_adminUserSeedOptions.Value.ActiveDatabase == ActiveDatabase.InMemoryDatabase)
            {
                context.Database.EnsureCreated();
            }
            else
            {
                context.Database.Migrate();
            }
        });
    }

    /// <summary>
    ///     Adds some default values to the IdentityDb
    /// </summary>
    public void SeedData()
    {
        _scopeFactory.RunScopedService<IIdentityDbInitializer>(identityDbSeedData =>
        {
            var result = identityDbSeedData.SeedDatabaseWithAdminUserAsync().GetAwaiter().GetResult();
            if (result == IdentityResult.Failed())
            {
                throw new InvalidOperationException(result.DumpErrors());
            }
        });

        _scopeFactory.RunScopedService<ApplicationDbContext>(context =>
        {
            if (!context.Roles.Any())
            {
                context.Add(new Role(ConstantRoles.Admin));
                context.SaveChanges();
            }
        });
    }

    public async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
    {
        var adminUserSeed = _adminUserSeedOptions.Value.AdminUserSeed;
        ArgumentNullException.ThrowIfNull(adminUserSeed);

        var name = adminUserSeed.Username;
        var password = adminUserSeed.Password;
        var email = adminUserSeed.Email;
        var roleName = adminUserSeed.RoleName;

        var thisMethodName = nameof(SeedDatabaseWithAdminUserAsync);

        var adminUser = await _applicationUserManager.FindByNameAsync(name);
        if (adminUser != null)
        {
            _logger.LogInformationMessage($"{thisMethodName}: adminUser already exists.");
            return IdentityResult.Success;
        }

        //Create the `Admin` Role if it does not exist
        var adminRole = await _roleManager.FindByNameAsync(roleName);
        if (adminRole == null)
        {
            adminRole = new Role(roleName);
            var adminRoleResult = await _roleManager.CreateAsync(adminRole);
            if (adminRoleResult == IdentityResult.Failed())
            {
                _logger.LogErrorMessage(
                    $"{thisMethodName}: adminRole CreateAsync failed. {adminRoleResult.DumpErrors()}");
                return IdentityResult.Failed();
            }
        }
        else
        {
            _logger.LogInformationMessage($"{thisMethodName}: adminRole already exists.");
        }

        adminUser = new User
        {
            UserName = name,
            Email = email,
            EmailConfirmed = true,
            IsEmailPublic = true,
            LockoutEnabled = true
        };
        var adminUserResult = await _applicationUserManager.CreateAsync(adminUser, password);
        if (adminUserResult == IdentityResult.Failed())
        {
            _logger.LogErrorMessage($"{thisMethodName}: adminUser CreateAsync failed. {adminUserResult.DumpErrors()}");
            return IdentityResult.Failed();
        }

        var setLockoutResult = await _applicationUserManager.SetLockoutEnabledAsync(adminUser, false);
        if (setLockoutResult == IdentityResult.Failed())
        {
            _logger.LogErrorMessage(
                $"{thisMethodName}: adminUser SetLockoutEnabledAsync failed. {setLockoutResult.DumpErrors()}");
            return IdentityResult.Failed();
        }

        var addToRoleResult = await _applicationUserManager.AddToRoleAsync(adminUser, adminRole.Name);
        if (addToRoleResult == IdentityResult.Failed())
        {
            _logger.LogErrorMessage(
                $"{thisMethodName}: adminUser AddToRoleAsync failed. {addToRoleResult.DumpErrors()}");
            return IdentityResult.Failed();
        }

        return IdentityResult.Success;
    }
}