using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using DNTCommon.Web.Core;
using System;
using DNTCaptcha.Core.Providers;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers
{
    [Area(AreaConstants.IdentityArea)]
    [AllowAnonymous]
    [BreadCrumb(Title = "ورود به سیستم", UseDefaultRouteUrl = true, Order = 0)]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

        public LoginController(
            IApplicationSignInManager signInManager,
            IApplicationUserManager userManager,
            IOptionsSnapshot<SiteSettings> siteOptions,
            ILogger<LoginController> logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        [NoBrowserCache]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(CaptchaGeneratorLanguage = Language.Persian,
                            CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbers)]
        public async Task<IActionResult> Index(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "نام کاربری و یا کلمه‌ی عبور وارد شده معتبر نیستند.");
                    return View(model);
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError(string.Empty, "اکانت شما غیرفعال شده‌است.");
                    return View(model);
                }

                if (_siteOptions.Value.EnableEmailConfirmation &&
                    !await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "لطفا به پست الکترونیک خود مراجعه کرده و ایمیل خود را تائید کنید!");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                                        model.Username,
                                        model.Password,
                                        model.RememberMe,
                                        lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"{model.Username} logged in.");
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(
                        nameof(TwoFactorController.SendCode),
                        "TwoFactor",
                        new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, $"{model.Username} قفل شده‌است.");
                    return View("~/Areas/Identity/Views/TwoFactor/Lockout.cshtml");
                }

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "عدم دسترسی ورود.");
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "نام کاربری و یا کلمه‌ی عبور وارد شده معتبر نیستند.");
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> LogOff()
        {
            var user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            await _signInManager.SignOutAsync();
            if (user != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                _logger.LogInformation(4, $"{user.UserName} logged out.");
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}