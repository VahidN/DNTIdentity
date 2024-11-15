using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2578
/// </summary>
public class ApplicationSignInManager(
    IApplicationUserManager userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<User> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<ApplicationSignInManager> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<User> confirmation) : SignInManager<User>((UserManager<User>)userManager, contextAccessor,
    claimsFactory, optionsAccessor, logger, schemes, confirmation), IApplicationSignInManager
{
    private readonly IHttpContextAccessor _contextAccessor =
        contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));

    #region BaseClass

    Task<bool> IApplicationSignInManager.IsLockedOut(User user) => base.IsLockedOut(user);

    Task<SignInResult> IApplicationSignInManager.LockedOut(User user) => base.LockedOut(user);

    Task<SignInResult> IApplicationSignInManager.PreSignInCheck(User user) => base.PreSignInCheck(user);

    Task IApplicationSignInManager.ResetLockout(User user) => base.ResetLockout(user);

    Task<SignInResult> IApplicationSignInManager.SignInOrTwoFactorAsync(User user,
        bool isPersistent,
        string loginProvider,
        bool bypassTwoFactor)
        => base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);

    #endregion

    #region CustomMethods

    public bool IsCurrentUserSignedIn() => IsSignedIn(_contextAccessor.HttpContext?.User);

    public Task<User> ValidateCurrentUserSecurityStampAsync()
        => ValidateSecurityStampAsync(_contextAccessor.HttpContext?.User);

    #endregion
}