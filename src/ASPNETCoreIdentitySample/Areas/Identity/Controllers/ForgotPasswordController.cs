using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Emails;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Area(AreaConstants.IdentityArea)]
[AllowAnonymous]
[BreadCrumb(Title = "بازیابی کلمه‌ی عبور", UseDefaultRouteUrl = true, Order = 0)]
public class ForgotPasswordController(
    IApplicationUserManager userManager,
    IPasswordValidator<User> passwordValidator,
    IEmailSender emailSender,
    IOptionsSnapshot<SiteSettings> siteOptions) : Controller
{
    [BreadCrumb(Title = "تائید کلمه‌ی عبور فراموش شده", Order = 1)]
    public IActionResult ForgotPasswordConfirmation() => View();

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public IActionResult Index() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha]
    public async Task<IActionResult> Index(ForgotPasswordViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.IsEmailConfirmedAsync(user))
            {
                return View(viewName: "Error");
            }

            var code = await userManager.GeneratePasswordResetTokenAsync(user);

            await emailSender.SendEmailAsync(model.Email, subject: "بازیابی کلمه‌ی عبور",
                viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_PasswordReset.cshtml",
                new PasswordResetViewModel
                {
                    UserId = user.Id,
                    Token = code,
                    EmailSignature = siteOptions.Value.Smtp.FromName,
                    MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                });

            return View(viewName: "ForgotPasswordConfirmation");
        }

        return View(model);
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidatePassword(string password, string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Json(data: "ایمیل وارد شده معتبر نیست.");
        }

        var result = await passwordValidator.ValidateAsync((UserManager<User>)userManager, user, password);

        return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
    }

    [BreadCrumb(Title = "تغییر کلمه‌ی عبور", Order = 1)]
    public IActionResult ResetPassword(string code = null) => code == null ? View(viewName: "Error") : View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        ModelState.AddModelError(key: "", result.DumpErrors(useHtmlNewLine: true));

        return View();
    }

    [BreadCrumb(Title = "تائیدیه تغییر کلمه‌ی عبور", Order = 1)]
    public IActionResult ResetPasswordConfirmation() => View();
}