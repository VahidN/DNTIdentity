using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Area(AreaConstants.IdentityArea)]
[AllowAnonymous]
[BreadCrumb(Title = "ورود به سیستم", UseDefaultRouteUrl = true, Order = 0)]
public class LoginController(
    IApplicationSignInManager signInManager,
    IApplicationUserManager userManager,
    IOptionsSnapshot<SiteSettings> siteOptions) : Controller
{
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    [NoBrowserCache]
    public IActionResult Index(string returnUrl = null)
    {
        ViewData[index: "ReturnUrl"] = returnUrl;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha]
    public async Task<IActionResult> Index(LoginViewModel model, string returnUrl = null)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        ViewData[index: "ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty,
                    errorMessage: "نام کاربری و یا کلمه‌ی عبور وارد شده معتبر نیستند.");

                return View(model);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError(string.Empty, errorMessage: "اکانت شما غیرفعال شده‌است.");

                return View(model);
            }

            if (siteOptions.Value.EnableEmailConfirmation && !await userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(key: "",
                    errorMessage: "لطفا به پست الکترونیک خود مراجعه کرده و ایمیل خود را تائید کنید!");

                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(TwoFactorController.SendCode), controllerName: "TwoFactor", new
                {
                    ReturnUrl = returnUrl,
                    model.RememberMe
                });
            }

            if (result.IsLockedOut)
            {
                return View(viewName: "~/Areas/Identity/Views/TwoFactor/Lockout.cshtml");
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, errorMessage: "عدم دسترسی ورود.");

                return View(model);
            }

            ModelState.AddModelError(string.Empty, errorMessage: "نام کاربری و یا کلمه‌ی عبور وارد شده معتبر نیستند.");

            return View(model);
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

    public async Task<IActionResult> LogOff()
    {
        var user = User.Identity is { IsAuthenticated: true }
            ? await userManager.FindByNameAsync(User.Identity.Name)
            : null;

        await signInManager.SignOutAsync();

        if (user != null)
        {
            await userManager.UpdateSecurityStampAsync(user);
        }

        return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
    }
}