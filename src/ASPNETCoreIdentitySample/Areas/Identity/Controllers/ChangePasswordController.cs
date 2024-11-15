using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Emails;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Authorize]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "تغییر کلمه‌ی عبور", UseDefaultRouteUrl = true, Order = 0)]
public class ChangePasswordController(
    IApplicationUserManager userManager,
    IApplicationSignInManager signInManager,
    IEmailSender emailSender,
    IPasswordValidator<User> passwordValidator,
    IUsedPasswordsService usedPasswordsService,
    IOptionsSnapshot<SiteSettings> siteOptions) : Controller
{
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index()
    {
        var userId = User.Identity?.GetUserId<int>() ?? 0;
        var passwordChangeDate = await usedPasswordsService.GetLastUserPasswordChangeDateAsync(userId);

        return View(new ChangePasswordViewModel
        {
            LastUserPasswordChangeDate = passwordChangeDate
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ChangePasswordViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.GetCurrentUserAsync();

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        if (result.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);

            // reflect the changes in the Identity cookie
            await signInManager.RefreshSignInAsync(user);

            await emailSender.SendEmailAsync(user.Email, subject: "اطلاع رسانی تغییر کلمه‌ی عبور",
                viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_ChangePasswordNotification.cshtml",
                new ChangePasswordNotificationViewModel
                {
                    User = user,
                    EmailSignature = siteOptions.Value.Smtp.FromName,
                    MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                });

            return RedirectToAction(nameof(Index), controllerName: "UserCard", new
            {
                id = user.Id
            });
        }

        ModelState.AddModelError(key: "", result.DumpErrors(useHtmlNewLine: true));

        return View(model);
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidatePassword(string newPassword)
    {
        var user = await userManager.GetCurrentUserAsync();
        var result = await passwordValidator.ValidateAsync((UserManager<User>)userManager, user, newPassword);

        return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
    }
}