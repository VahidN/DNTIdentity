using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Emails;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers
{
    [Authorize]
    [Area(AreaConstants.IdentityArea)]
    [BreadCrumb(Title = "تغییر کلمه‌ی عبور", UseDefaultRouteUrl = true, Order = 0)]
    public class ChangePasswordController: Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IUsedPasswordsService _usedPasswordsService;
        private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

        public ChangePasswordController(
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IEmailSender emailSender,
            IPasswordValidator<User> passwordValidator,
            IUsedPasswordsService usedPasswordsService,
            IOptionsSnapshot<SiteSettings> siteOptions)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _signInManager = signInManager;
            _signInManager.CheckArgumentIsNull(nameof(_signInManager));

            _passwordValidator = passwordValidator;
            _passwordValidator.CheckArgumentIsNull(nameof(_passwordValidator));

            _usedPasswordsService = usedPasswordsService;
            _usedPasswordsService.CheckArgumentIsNull(nameof(_usedPasswordsService));

            _emailSender = emailSender;
            _emailSender.CheckArgumentIsNull(nameof(_emailSender));

            _siteOptions = siteOptions;
            _siteOptions.CheckArgumentIsNull(nameof(_siteOptions));
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.Identity.GetUserId<int>();
            var passwordChangeDate = await _usedPasswordsService.GetLastUserPasswordChangeDateAsync(userId).ConfigureAwait(false);
            return View(model: new ChangePasswordViewModel
            {
                LastUserPasswordChangeDate = passwordChangeDate
            });
        }

        /// <summary>
        /// For [Remote] validation
        /// </summary>
        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePassword(string newPassword)
        {
            var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, user, newPassword).ConfigureAwait(false);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false);

                // reflect the changes in the Identity cookie
                await _signInManager.RefreshSignInAsync(user).ConfigureAwait(false);

                await _emailSender.SendEmailAsync(
                           email: user.Email,
                           subject: "اطلاع رسانی تغییر کلمه‌ی عبور",
                           viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_ChangePasswordNotification.cshtml",
                           model: new ChangePasswordNotificationViewModel
                           {
                               User = user,
                               EmailSignature = _siteOptions.Value.Smtp.FromName,
                               MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                           }).ConfigureAwait(false);

                return RedirectToAction(nameof(Index), "UserCard", routeValues: new { id = user.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}