using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     Keep track of on-line users
/// </summary>
public class CustomSecurityStampValidator(
    IOptions<SecurityStampValidatorOptions> options,
    IApplicationSignInManager signInManager,
    ISiteStatService siteStatService,
    ILoggerFactory logger) : SecurityStampValidator<User>(options, (SignInManager<User>)signInManager, logger)
{
    private readonly ISiteStatService _siteStatService =
        siteStatService ?? throw new ArgumentNullException(nameof(siteStatService));

    public TimeSpan UpdateLastModifiedDate { get; set; } = TimeSpan.FromMinutes(minutes: 2);

    public override async Task ValidateAsync(CookieValidatePrincipalContext context)
    {
        await base.ValidateAsync(context);
        await UpdateUserLastVisitDateTimeAsync(context);
    }

    private async Task UpdateUserLastVisitDateTimeAsync(CookieValidatePrincipalContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var currentUtc = DateTimeOffset.UtcNow;

        if (context.Options != null)
        {
            currentUtc = TimeProvider.GetUtcNow();
        }

        var issuedUtc = context.Properties.IssuedUtc;

        // Only validate if enough time has elapsed
        if (issuedUtc == null || context.Principal == null)
        {
            return;
        }

        var timeElapsed = currentUtc.Subtract(issuedUtc.Value);

        if (timeElapsed > UpdateLastModifiedDate)
        {
            await _siteStatService.UpdateUserLastVisitDateTimeAsync(context.Principal);
        }
    }
}