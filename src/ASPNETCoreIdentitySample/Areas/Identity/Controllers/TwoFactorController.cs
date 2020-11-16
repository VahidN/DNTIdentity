using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Emails;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers
{
    [Authorize]
    [Area(AreaConstants.IdentityArea)]
    [BreadCrumb(Title = "اعتبارسنجی دو مرحله‌ای", UseDefaultRouteUrl = true, Order = 0)]
    public class TwoFactorController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<TwoFactorController> _logger;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

        public TwoFactorController(
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IEmailSender emailSender,
            IOptionsSnapshot<SiteSettings> siteOptions,
            ILogger<TwoFactorController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [BreadCrumb(Title = "ارسال کد", Order = 1)]
        public async Task<IActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("NotFound");
            }

            var tokenProvider = "Email";
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, tokenProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return View("Error");
            }

            await _emailSender.SendEmailAsync(
                               email: user.Email,
                               subject: "کد جدید اعتبارسنجی دو مرحله‌ای",
                               viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_TwoFactorSendCode.cshtml",
                               model: new TwoFactorSendCodeViewModel
                               {
                                   Token = code,
                                   EmailSignature = _siteOptions.Value.Smtp.FromName,
                                   MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                               });

            return RedirectToAction(
                nameof(VerifyCode),
                new
                {
                    Provider = tokenProvider,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                });
        }

        [AllowAnonymous]
        [BreadCrumb(Title = "تائید کد", Order = 1)]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("NotFound");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(
                model.Provider,
                model.Code,
                model.RememberMe,
                model.RememberBrowser);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }

            ModelState.AddModelError(string.Empty, "کد وارد شده غیر معتبر است.");
            return View(model);
        }
    }
}