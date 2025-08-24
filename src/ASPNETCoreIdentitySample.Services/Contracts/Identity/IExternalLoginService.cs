using ASPNETCoreIdentitySample.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentitySample.Services.Contracts.Identity;
public interface IExternalLoginService
{
    Task<IdentityResult> AddExternalLoginAsync(User user, ExternalLoginInfo info);
}
