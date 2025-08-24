using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Area(AreaConstants.IdentityArea)]
[Authorize]
public class ExternalLoginController(
    IApplicationSignInManager signInManager,
    IApplicationUserManager userManager,
    IExternalLoginService externalLoginService) : Controller
{
    private IActionResult LocalRedirectSafely(string returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    [AllowAnonymous]
    public Task<IActionResult> Challenge(string provider, string returnUrl = null)
    {
        var redirectUrl = Url.Action(nameof(Callback), "ExternalLogin", new { returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Task.FromResult<IActionResult>(Challenge(properties, provider));
    }

    [AllowAnonymous]
    public async Task<IActionResult> Callback(string returnUrl = null)
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null) return RedirectToAction(nameof(LoginController.Index), "Login");

        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

        if (result.Succeeded)
        {
            return LocalRedirectSafely(returnUrl);
        }

        if (result.IsNotAllowed)
        {
            var existingLoginUser = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (existingLoginUser != null)
            {
                if (!existingLoginUser.EmailConfirmed)
                {
                    existingLoginUser.EmailConfirmed = true;
                    await userManager.UpdateAsync(existingLoginUser);
                }
                await signInManager.SignInAsync(existingLoginUser, isPersistent: false);
                return LocalRedirectSafely(returnUrl);
            }
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        if (!string.IsNullOrEmpty(email))
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser is { EmailConfirmed: true })
            {
                var addLoginResult = await externalLoginService.AddExternalLoginAsync(existingUser, info);
                if (addLoginResult.Succeeded)
                {
                    await signInManager.SignInAsync(existingUser, isPersistent: false);
                    return LocalRedirectSafely(returnUrl);
                }
            }
        }

        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
        {
            Email = email,
            ReturnUrl = returnUrl,
            ProviderDisplayName = info.ProviderDisplayName ?? info.LoginProvider
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model)
    {
        if (model == null)
        {
            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = string.Empty });
        }
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            TempData["Error"] = "اطلاعات ورود خارجی یافت نشد.";
            return RedirectToAction(nameof(LoginController.Index), "Login");
        }

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            user = new Entities.Identity.User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = true,
                EmailConfirmed = true
            };
            var createResult = await userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                foreach (var err in createResult.Errors) ModelState.AddModelError(string.Empty, err.Description);
                return View(model);
            }
        }
        else if (!user.EmailConfirmed)
        {
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
        }

        var addLoginResult = await externalLoginService.AddExternalLoginAsync(user, info);
        if (!addLoginResult.Succeeded)
        {
            foreach (var err in addLoginResult.Errors) ModelState.AddModelError(string.Empty, err.Description);
            return View(model);
        }

        await signInManager.SignInAsync(user, isPersistent: false);
        return LocalRedirectSafely(model.ReturnUrl);
    }
}
