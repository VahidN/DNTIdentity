using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Common.GuardToolkit;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    public class ApplicationSignInManager :
        SignInManager<User>,
        IApplicationSignInManager
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly ILogger<ApplicationSignInManager> _logger;

        public ApplicationSignInManager(
            IApplicationUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<ApplicationSignInManager> logger)
            : base((UserManager<User>)userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

            _claimsFactory = claimsFactory;
            _claimsFactory.CheckArgumentIsNull(nameof(_claimsFactory));

            _optionsAccessor = optionsAccessor;
            _optionsAccessor.CheckArgumentIsNull(nameof(_optionsAccessor));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(_logger));
        }

        #region BaseClass

        #endregion

        #region CustomMethods

        public bool IsCurrentUserSignedIn()
        {
            return IsSignedIn(_contextAccessor.HttpContext.User);
        }

        public Task<User> ValidateCurrentUserSecurityStampAsync()
        {
            return ValidateSecurityStampAsync(_contextAccessor.HttpContext.User);
        }

        #endregion
    }
}
