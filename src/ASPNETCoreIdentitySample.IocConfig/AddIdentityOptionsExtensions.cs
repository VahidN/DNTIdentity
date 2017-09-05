using System;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.IocConfig
{
    public static class AddIdentityOptionsExtensions
    {
        public const string EmailConfirmationTokenProviderName = "ConfirmEmail";

        public static IServiceCollection AddIdentityOptions(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));

            services.addConfirmEmailDataProtectorTokenOptions(siteSettings);
            services.AddIdentity<User, Role>(identityOptions =>
            {
                setPasswordOptions(identityOptions.Password, siteSettings);
                setSignInOptions(identityOptions.SignIn, siteSettings);
                setUserOptions(identityOptions.User);
                setLockoutOptions(identityOptions.Lockout, siteSettings);
            }).AddUserStore<ApplicationUserStore>()
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
                setApplicationCookieOptions(provider, identityOptionsCookies, siteSettings);
            });

            services.enableIimmediateLogout();

            return services;
        }

        private static void addConfirmEmailDataProtectorTokenOptions(this IServiceCollection services, SiteSettings siteSettings)
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

        private static void enableIimmediateLogout(this IServiceCollection services)
        {
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
                options.OnRefreshingPrincipal = principalContext =>
                {
                    // Invoked when the default security stamp validator replaces the user's ClaimsPrincipal in the cookie.

                    //var newId = new ClaimsIdentity();
                    //newId.AddClaim(new Claim("PreviousName", principalContext.CurrentPrincipal.Identity.Name));
                    //principalContext.NewPrincipal.AddIdentity(newId);

                    return Task.CompletedTask;
                };
            });
        }

        private static void setApplicationCookieOptions(IServiceProvider provider, CookieAuthenticationOptions identityOptionsCookies, SiteSettings siteSettings)
        {
            identityOptionsCookies.Cookie.Name = siteSettings.CookieOptions.CookieName;
            identityOptionsCookies.Cookie.HttpOnly = true;
            identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            identityOptionsCookies.Cookie.SameSite = SameSiteMode.Lax;
            identityOptionsCookies.ExpireTimeSpan = siteSettings.CookieOptions.ExpireTimeSpan;
            identityOptionsCookies.SlidingExpiration = siteSettings.CookieOptions.SlidingExpiration;
            identityOptionsCookies.LoginPath = siteSettings.CookieOptions.LoginPath;
            identityOptionsCookies.LogoutPath = siteSettings.CookieOptions.LogoutPath;
            identityOptionsCookies.AccessDeniedPath = siteSettings.CookieOptions.AccessDeniedPath;

            var ticketStore = provider.GetService<ITicketStore>();
            ticketStore.CheckArgumentIsNull(nameof(ticketStore));
            identityOptionsCookies.SessionStore = ticketStore; // To manage large identity cookies
        }

        private static void setLockoutOptions(LockoutOptions identityOptionsLockout, SiteSettings siteSettings)
        {
            identityOptionsLockout.AllowedForNewUsers = siteSettings.LockoutOptions.AllowedForNewUsers;
            identityOptionsLockout.DefaultLockoutTimeSpan = siteSettings.LockoutOptions.DefaultLockoutTimeSpan;
            identityOptionsLockout.MaxFailedAccessAttempts = siteSettings.LockoutOptions.MaxFailedAccessAttempts;
        }

        private static void setPasswordOptions(PasswordOptions identityOptionsPassword, SiteSettings siteSettings)
        {
            identityOptionsPassword.RequireDigit = siteSettings.PasswordOptions.RequireDigit;
            identityOptionsPassword.RequireLowercase = siteSettings.PasswordOptions.RequireLowercase;
            identityOptionsPassword.RequireNonAlphanumeric = siteSettings.PasswordOptions.RequireNonAlphanumeric;
            identityOptionsPassword.RequireUppercase = siteSettings.PasswordOptions.RequireUppercase;
            identityOptionsPassword.RequiredLength = siteSettings.PasswordOptions.RequiredLength;
        }

        private static void setSignInOptions(SignInOptions identityOptionsSignIn, SiteSettings siteSettings)
        {
            identityOptionsSignIn.RequireConfirmedEmail = siteSettings.EnableEmailConfirmation;
        }

        private static void setUserOptions(UserOptions identityOptionsUser)
        {
            identityOptionsUser.RequireUniqueEmail = true;
        }
    }
}