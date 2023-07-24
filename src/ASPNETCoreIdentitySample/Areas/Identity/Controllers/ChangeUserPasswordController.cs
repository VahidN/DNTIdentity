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
public class ChangeUserPasswordController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IApplicationSignInManager _signInManager;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
    private readonly IUsedPasswordsService _usedPasswordsService;
    private readonly IApplicationUserManager _userManager;

    public ChangeUserPasswordController(
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
    public async Task<IActionResult> Index(int? id)
    {
        if (!id.HasValue)
        {
            return View("NotFound");
        }

        var user = await _userManager.FindByIdAsync(id.Value.ToString(CultureInfo.InvariantCulture));
        if (user == null)
        {
            return View("NotFound");
        }

        return View(new ChangeUserPasswordViewModel
                    {
                        UserId = user.Id,
                        Name = user.UserName,
                    });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ChangeUserPasswordViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(model.UserId.ToString(CultureInfo.InvariantCulture));
        if (user == null)
        {
            return View("NotFound");
        }

        var result = await _userManager.UpdatePasswordHash(user, model.NewPassword, true);
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
                                                  MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString(),
                                              });

            return RedirectToAction(nameof(Index), "UserCard", new { id = user.Id });
        }

        ModelState.AddModelError("", result.DumpErrors(true));

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
        var user = await _userManager.FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));
        var result = await _passwordValidator.ValidateAsync(
                                                            (UserManager<User>)_userManager,
                                                            user,
                                                            newPassword);
        return Json(result.Succeeded ? "true" : result.DumpErrors(true));
    }
}