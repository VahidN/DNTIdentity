using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Emails;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTBreadCrumb.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Authorize]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "اعتبارسنجی دو مرحله‌ای", UseDefaultRouteUrl = true, Order = 0)]
public class TwoFactorController(
    IApplicationUserManager userManager,
    IApplicationSignInManager signInManager,
    IEmailSender emailSender,
    IOptionsSnapshot<SiteSettings> siteOptions) : Controller
{
    [AllowAnonymous]
    [BreadCrumb(Title = "ارسال کد", Order = 1)]
    public async Task<IActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
    {
        var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        var tokenProvider = "Email";
        var code = await userManager.GenerateTwoFactorTokenAsync(user, tokenProvider);

        if (string.IsNullOrWhiteSpace(code))
        {
            return View(viewName: "Error");
        }

        await emailSender.SendEmailAsync(user.Email, subject: "کد جدید اعتبارسنجی دو مرحله‌ای",
            viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_TwoFactorSendCode.cshtml",
            new TwoFactorSendCodeViewModel
            {
                Token = code,
                EmailSignature = siteOptions.Value.Smtp.FromName,
                MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
            });

        return RedirectToAction(nameof(VerifyCode), new
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
        var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        return View(new VerifyCodeViewModel
        {
            Provider = provider,
            ReturnUrl = returnUrl,
            RememberMe = rememberMe
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // The following code protects for brute force attacks against the two factor codes.
        // If a user enters incorrect codes for a specified amount of time then the user account
        // will be locked out for a specified amount of time.
        var result = await signInManager.TwoFactorSignInAsync(
            model.Provider, model.Code, model.RememberMe, model.RememberBrowser);

        if (result.Succeeded)
        {
            var returnUrl = model.ReturnUrl;

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
        }

        if (result.IsLockedOut)
        {
            return View(viewName: "Lockout");
        }

        ModelState.AddModelError(string.Empty, errorMessage: "کد وارد شده غیر معتبر است.");

        return View(model);
    }
}