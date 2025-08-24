using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentitySample.Services.Identity;
public sealed class ExternalLoginService(IApplicationUserManager userManager) : IExternalLoginService
{
    public async Task<IdentityResult> AddExternalLoginAsync(User user, ExternalLoginInfo info)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(info);
        return await userManager.AddLoginAsync(user, info);
    }
}
