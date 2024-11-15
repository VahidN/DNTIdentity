using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     How to override the default (1 day) TokenLifeSpan for the email confirmations.
/// </summary>
public class ConfirmEmailDataProtectorTokenProvider<TUser>(
    IDataProtectionProvider dataProtectionProvider,
    IOptions<ConfirmEmailDataProtectionTokenProviderOptions> options,
    ILogger<ConfirmEmailDataProtectorTokenProvider<TUser>> logger)
    : DataProtectorTokenProvider<TUser>(dataProtectionProvider, options, logger)
    where TUser : class
{
    // NOTE: DataProtectionTokenProviderOptions.TokenLifespan is set to TimeSpan.FromDays(1)
    // which is low for the `ConfirmEmail` task.
}