using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
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

[Authorize(Roles = ConstantRoles.Admin)]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "تغییر کلمه‌ی عبور كاربر توسط مدير سيستم", UseDefaultRouteUrl = true, Order = 0)]
public class ChangeUserPasswordController(
    IApplicationUserManager userManager,
    IApplicationSignInManager signInManager,
    IEmailSender emailSender,
    IPasswordValidator<User> passwordValidator,
    IOptionsSnapshot<SiteSettings> siteOptions) : Controller
{
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index(int? id)
    {
        if (!id.HasValue)
        {
            return View(viewName: "NotFound");
        }

        var user = await userManager.FindByIdAsync(id.Value.ToString(CultureInfo.InvariantCulture));

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        return View(new ChangeUserPasswordViewModel
        {
            UserId = user.Id,
            Name = user.UserName
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ChangeUserPasswordViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.FindByIdAsync(model.UserId.ToString(CultureInfo.InvariantCulture));

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        var result = await userManager.UpdatePasswordHash(user, model.NewPassword, validatePassword: true);

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
    public async Task<IActionResult> ValidatePassword(string newPassword, int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));
        var result = await passwordValidator.ValidateAsync((UserManager<User>)userManager, user, newPassword);

        return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
    }
}