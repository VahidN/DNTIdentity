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

[Authorize, Area(AreaConstants.IdentityArea),
 BreadCrumb(Title = "تغییر کلمه‌ی عبور", UseDefaultRouteUrl = true, Order = 0)]
public class ChangePasswordController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IApplicationSignInManager _signInManager;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
    private readonly IUsedPasswordsService _usedPasswordsService;
    private readonly IApplicationUserManager _userManager;

    public ChangePasswordController(
        IApplicationUserManager userManager,
        IApplicationSignInManager signInManager,
        IEmailSender emailSender,
        IPasswordValidator<User> passwordValidator,
        IUsedPasswordsService usedPasswordsService,
        IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _passwordValidator = passwordValidator ?? throw new ArgumentNullException(nameof(passwordValidator));
        _usedPasswordsService = usedPasswordsService ?? throw new ArgumentNullException(nameof(usedPasswordsService));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
    }

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index()
    {
        var userId = User.Identity?.GetUserId<int>() ?? 0;
        var passwordChangeDate = await _usedPasswordsService.GetLastUserPasswordChangeDateAsync(userId);
        return View(new ChangePasswordViewModel
        {
            LastUserPasswordChangeDate = passwordChangeDate
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ChangePasswordViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetCurrentUserAsync();
        if (user == null)
        {
            return View("NotFound");
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);

            // reflect the changes in the Identity cookie
            await _signInManager.RefreshSignInAsync(user);

            await _emailSender.SendEmailAsync(
                user.Email,
                "اطلاع رسانی تغییر کلمه‌ی عبور",
                "~/Areas/Identity/Views/EmailTemplates/_ChangePasswordNotification.cshtml",
                new ChangePasswordNotificationViewModel
                {
                    User = user,
                    EmailSignature = _siteOptions.Value.Smtp.FromName,
                    MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                });

            return RedirectToAction(nameof(Index), "UserCard", new { id = user.Id });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidatePassword(string newPassword)
    {
        var user = await _userManager.GetCurrentUserAsync();
        var result = await _passwordValidator.ValidateAsync(
            (UserManager<User>)_userManager, user, newPassword);
        return Json(result.Succeeded ? "true" : result.DumpErrors(true));
    }
}