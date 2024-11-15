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
[BreadCrumb(Title = "ثبت نام", UseDefaultRouteUrl = true, Order = 0)]
public class RegisterController(
    IApplicationUserManager userManager,
    IPasswordValidator<User> passwordValidator,
    IUserValidator<User> userValidator,
    IEmailSender emailSender,
    IOptionsSnapshot<SiteSettings> siteOptions) : Controller
{
    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidateUsername(string username, string email)
    {
        var result = await userValidator.ValidateAsync((UserManager<User>)userManager, new User
        {
            UserName = username,
            Email = email
        });

        return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidatePassword(string password, string username)
    {
        var result = await passwordValidator.ValidateAsync((UserManager<User>)userManager, new User
        {
            UserName = username
        }, password);

        return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
    }

    [BreadCrumb(Title = "تائید ایمیل", Order = 1)]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return View(viewName: "Error");
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        var result = await userManager.ConfirmEmailAsync(user, code);

        return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
    }

    [BreadCrumb(Title = "تائیدیه ایمیل", Order = 1)]
    public IActionResult ConfirmedRegisteration() => View();

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public IActionResult Index() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (siteOptions.Value.EnableEmailConfirmation)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    //ControllerExtensions.ShortControllerName<RegisterController>(), //TODO: use everywhere .................

                    await emailSender.SendEmailAsync(user.Email, subject: "لطفا اکانت خود را تائید کنید",
                        viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_RegisterEmailConfirmation.cshtml",
                        new RegisterEmailConfirmationViewModel
                        {
                            User = user,
                            EmailConfirmationToken = code,
                            EmailSignature = siteOptions.Value.Smtp.FromName,
                            MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                        });

                    return RedirectToAction(nameof(ConfirmYourEmail));
                }

                return RedirectToAction(nameof(ConfirmedRegisteration));
            }

            ModelState.AddModelError(key: "", result.DumpErrors(useHtmlNewLine: true));
        }

        return View(model);
    }

    [BreadCrumb(Title = "ایمیل خود را تائید کنید", Order = 1)]
    public IActionResult ConfirmYourEmail() => View();
}