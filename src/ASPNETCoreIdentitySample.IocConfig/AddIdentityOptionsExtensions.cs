using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.IocConfig;

public static class AddIdentityOptionsExtensions
{
    public const string EmailConfirmationTokenProviderName = "ConfirmEmail";

    public static IServiceCollection AddIdentityOptions(this IServiceCollection services, SiteSettings siteSettings)
    {
        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        services.AddConfirmEmailDataProtectorTokenOptions(siteSettings);

        services.AddIdentity<User, Role>(identityOptions =>
            {
                SetPasswordOptions(identityOptions.Password, siteSettings);
                SetSignInOptions(identityOptions.SignIn, siteSettings);
                SetUserOptions(identityOptions.User);
                SetLockoutOptions(identityOptions.Lockout, siteSettings);
            })
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()

            // You **cannot** use .AddEntityFrameworkStores() when you customize everything
            //.AddEntityFrameworkStores<ApplicationDbContext, int>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<ConfirmEmailDataProtectorTokenProvider<User>>(EmailConfirmationTokenProviderName);

        services.ConfigureApplicationCookie(identityOptionsCookies =>
        {
            var provider = services.BuildServiceProvider();
            SetApplicationCookieOptions(provider, identityOptionsCookies, siteSettings);
        });

        services.EnableImmediateLogout();

        return services;
    }

    private static void AddConfirmEmailDataProtectorTokenOptions(this IServiceCollection services,
        SiteSettings siteSettings)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
        });

        services.Configure<ConfirmEmailDataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = siteSettings.EmailConfirmationTokenProviderLifespan;
        });
    }

    private static void EnableImmediateLogout(this IServiceCollection services)
        => services.Configure<SecurityStampValidatorOptions>(options =>
        {
            // enables immediate logout, after updating the user's stat.
            options.ValidationInterval = TimeSpan.Zero;

            options.OnRefreshingPrincipal = principalContext =>
            {
                // Invoked when the default security stamp validator replaces the user's ClaimsPrincipal in the cookie.

                //var newId = new ClaimsIdentity()
                //newId.AddClaim(new Claim("PreviousName", principalContext.CurrentPrincipal.Identity.Name))
                //principalContext.NewPrincipal.AddIdentity(newId)

                return Task.CompletedTask;
            };
        });

    private static void SetApplicationCookieOptions(IServiceProvider provider,
        CookieAuthenticationOptions identityOptionsCookies,
        SiteSettings siteSettings)
    {
        identityOptionsCookies.Cookie.Name = siteSettings.CookieOptions.CookieName;
        identityOptionsCookies.Cookie.HttpOnly = true;
        identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        identityOptionsCookies.Cookie.SameSite = SameSiteMode.Lax;

        identityOptionsCookies.Cookie.IsEssential =
            true; //  this cookie will always be stored regardless of the user's consent

        identityOptionsCookies.ExpireTimeSpan = siteSettings.CookieOptions.ExpireTimeSpan;
        identityOptionsCookies.SlidingExpiration = siteSettings.CookieOptions.SlidingExpiration;
        identityOptionsCookies.LoginPath = siteSettings.CookieOptions.LoginPath;
        identityOptionsCookies.LogoutPath = siteSettings.CookieOptions.LogoutPath;
        identityOptionsCookies.AccessDeniedPath = siteSettings.CookieOptions.AccessDeniedPath;

        if (siteSettings.CookieOptions.UseDistributedCacheTicketStore)
        {
            // To manage large identity cookies
            identityOptionsCookies.SessionStore = provider.GetRequiredService<ITicketStore>();
        }
    }

    private static void SetLockoutOptions(LockoutOptions identityOptionsLockout, SiteSettings siteSettings)
    {
        identityOptionsLockout.AllowedForNewUsers = siteSettings.LockoutOptions.AllowedForNewUsers;
        identityOptionsLockout.DefaultLockoutTimeSpan = siteSettings.LockoutOptions.DefaultLockoutTimeSpan;
        identityOptionsLockout.MaxFailedAccessAttempts = siteSettings.LockoutOptions.MaxFailedAccessAttempts;
    }

    private static void SetPasswordOptions(PasswordOptions identityOptionsPassword, SiteSettings siteSettings)
    {
        identityOptionsPassword.RequireDigit = siteSettings.PasswordOptions.RequireDigit;
        identityOptionsPassword.RequireLowercase = siteSettings.PasswordOptions.RequireLowercase;
        identityOptionsPassword.RequireNonAlphanumeric = siteSettings.PasswordOptions.RequireNonAlphanumeric;
        identityOptionsPassword.RequireUppercase = siteSettings.PasswordOptions.RequireUppercase;
        identityOptionsPassword.RequiredLength = siteSettings.PasswordOptions.RequiredLength;
    }

    private static void SetSignInOptions(SignInOptions identityOptionsSignIn, SiteSettings siteSettings)
        => identityOptionsSignIn.RequireConfirmedEmail = siteSettings.EnableEmailConfirmation;

    private static void SetUserOptions(UserOptions identityOptionsUser)
        => identityOptionsUser.RequireUniqueEmail = true;
}