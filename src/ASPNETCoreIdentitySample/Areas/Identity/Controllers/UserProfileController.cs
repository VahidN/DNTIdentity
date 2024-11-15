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

[Authorize]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "مشخصات کاربری", UseDefaultRouteUrl = true, Order = 0)]
public class UserProfileController(
    IApplicationUserManager userManager,
    IApplicationRoleManager roleManager,
    IApplicationSignInManager signInManager,
    IProtectionProviderService protectionProviderService,
    IUserValidator<User> userValidator,
    IUsedPasswordsService usedPasswordsService,
    IUsersPhotoService usersPhotoService,
    IOptionsSnapshot<SiteSettings> siteOptions,
    IEmailSender emailSender,
    ILogger<UserProfileController> logger) : Controller
{
    [Authorize(Roles = ConstantRoles.Admin)]
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> AdminEdit(int? id)
    {
        if (!id.HasValue)
        {
            return View(viewName: "Error");
        }

        var user = await userManager.FindByIdAsync(id.Value.ToString(CultureInfo.InvariantCulture));

        return await RenderForm(user, isAdminEdit: true);
    }

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetCurrentUserAsync();

        return await RenderForm(user, isAdminEdit: false);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(UserProfileViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        if (ModelState.IsValid)
        {
            var pid = protectionProviderService.Decrypt(model.Pid);

            if (string.IsNullOrWhiteSpace(pid))
            {
                return View(viewName: "Error");
            }

            if (!string.Equals(pid, userManager.GetCurrentUserId(), StringComparison.Ordinal) &&
                !roleManager.IsCurrentUserInRole(ConstantRoles.Admin))
            {
                logger.LogWarningMessage($"سعی در دسترسی غیرمجاز به ویرایش اطلاعات کاربر {pid}");

                return View(viewName: "Error");
            }

            var user = await userManager.FindByIdAsync(pid);

            if (user == null)
            {
                return View(viewName: "NotFound");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsEmailPublic = model.IsEmailPublic;
            user.TwoFactorEnabled = model.TwoFactorEnabled;
            user.Location = model.Location;

            UpdateUserBirthDate(model, user);

            if (!await UpdateUserName(model, user))
            {
                return View(nameof(Index), model);
            }

            if (!await UpdateUserAvatarImage(model, user))
            {
                return View(nameof(Index), model);
            }

            if (!await UpdateUserEmail(model, user))
            {
                return View(nameof(Index), model);
            }

            var updateResult = await userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                if (!model.IsAdminEdit)
                {
                    // reflect the changes in the current user's Identity cookie
                    await signInManager.RefreshSignInAsync(user);
                }

                await emailSender.SendEmailAsync(user.Email, subject: "اطلاع رسانی به روز رسانی مشخصات کاربری",
                    viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_UserProfileUpdateNotification.cshtml",
                    new UserProfileUpdateNotificationViewModel
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

            ModelState.AddModelError(key: "", updateResult.DumpErrors(useHtmlNewLine: true));
        }

        return View(nameof(Index), model);
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidateUsername(string username, string email, string pid)
    {
        pid = protectionProviderService.Decrypt(pid);

        if (string.IsNullOrWhiteSpace(pid))
        {
            return Json(data: "اطلاعات وارد شده معتبر نیست.");
        }

        var user = await userManager.FindByIdAsync(pid);
        user.UserName = username;
        user.Email = email;

        var result = await userValidator.ValidateAsync((UserManager<User>)userManager, user);

        return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
    }

    private static void UpdateUserBirthDate(UserProfileViewModel model, User user)
    {
        if (model.DateOfBirthYear.HasValue && model.DateOfBirthMonth.HasValue && model.DateOfBirthDay.HasValue)
        {
            var date =
                $"{model.DateOfBirthYear.Value.ToString(CultureInfo.InvariantCulture)}/{model.DateOfBirthMonth.Value.ToString(format: "00", CultureInfo.InvariantCulture)}/{model.DateOfBirthDay.Value.ToString(format: "00", CultureInfo.InvariantCulture)}";

            user.BirthDate = date.ToGregorianDateTime(convertToUtc: true);
        }
        else
        {
            user.BirthDate = null;
        }
    }

    private async Task<IActionResult> RenderForm(User user, bool isAdminEdit)
    {
        usersPhotoService.SetUserDefaultPhoto(user);

        var userProfile = new UserProfileViewModel
        {
            IsAdminEdit = isAdminEdit,
            Email = user.Email,
            PhotoFileName = user.PhotoFileName,
            Location = user.Location,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Pid = protectionProviderService.Encrypt(user.Id.ToString(CultureInfo.InvariantCulture)),
            IsEmailPublic = user.IsEmailPublic,
            TwoFactorEnabled = user.TwoFactorEnabled,
            IsPasswordTooOld = await usedPasswordsService.IsLastUserPasswordTooOldAsync(user.Id)
        };

        if (user.BirthDate.HasValue)
        {
            var pDateParts = user.BirthDate.Value.ToPersianYearMonthDay();
            userProfile.DateOfBirthYear = pDateParts.Year;
            userProfile.DateOfBirthMonth = pDateParts.Month;
            userProfile.DateOfBirthDay = pDateParts.Day;
        }

        return View(nameof(Index), userProfile);
    }

    private async Task<bool> UpdateUserAvatarImage(UserProfileViewModel model, User user)
    {
        usersPhotoService.SetUserDefaultPhoto(user);

        var photoFile = model.Photo;

        if (photoFile is not null && photoFile.Length > 0)
        {
            var imageOptions = siteOptions.Value.UserAvatarImageOptions;

            if (!photoFile.IsValidImageFile(imageOptions.MaxWidth, imageOptions.MaxHeight))
            {
                ModelState.AddModelError(key: "",
                    Invariant(
                        $"حداکثر اندازه تصویر قابل ارسال {imageOptions.MaxHeight} در {imageOptions.MaxWidth} پیکسل است"));

                model.PhotoFileName = user.PhotoFileName;

                return false;
            }

            var uploadsRootFolder = usersPhotoService.GetUsersAvatarsFolderPath();
            var photoFileName = Invariant($"{user.Id}{Path.GetExtension(photoFile.FileName)}");
            var filePath = Path.Combine(uploadsRootFolder, photoFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(fileStream);
            }

            user.PhotoFileName = photoFileName;
        }

        return true;
    }

    private async Task<bool> UpdateUserEmail(UserProfileViewModel model, User user)
    {
        if (!string.Equals(user.Email, model.Email, StringComparison.Ordinal))
        {
            user.Email = model.Email;
            var validator = await userValidator.ValidateAsync((UserManager<User>)userManager, user);

            if (!validator.Succeeded)
            {
                ModelState.AddModelError(key: "", validator.DumpErrors(useHtmlNewLine: true));

                return false;
            }

            user.EmailConfirmed = false;

            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            await emailSender.SendEmailAsync(user.Email, subject: "لطفا اکانت خود را تائید کنید",
                viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_RegisterEmailConfirmation.cshtml",
                new RegisterEmailConfirmationViewModel
                {
                    User = user,
                    EmailConfirmationToken = code,
                    EmailSignature = siteOptions.Value.Smtp.FromName,
                    MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                });
        }

        return true;
    }

    private async Task<bool> UpdateUserName(UserProfileViewModel model, User user)
    {
        if (!string.Equals(user.UserName, model.UserName, StringComparison.Ordinal))
        {
            user.UserName = model.UserName;
            var validator = await userValidator.ValidateAsync((UserManager<User>)userManager, user);

            if (!validator.Succeeded)
            {
                ModelState.AddModelError(key: "", validator.DumpErrors(useHtmlNewLine: true));

                return false;
            }
        }

        return true;
    }
}