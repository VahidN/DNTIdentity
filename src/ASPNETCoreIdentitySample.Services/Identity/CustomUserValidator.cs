﻿using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     Extending the Built-in User Validation
///     More info: http://www.dntips.ir/post/2579
/// </summary>
public class CustomUserValidator : UserValidator<User>
{
    private readonly ISet<string> _emailsBanList;

    public CustomUserValidator(IdentityErrorDescriber errors, // How to use CustomIdentityErrorDescriber
        IOptionsSnapshot<SiteSettings> configurationRoot) : base(errors)
    {
        if (configurationRoot == null)
        {
            throw new ArgumentNullException(nameof(configurationRoot));
        }

        _emailsBanList = new HashSet<string>(configurationRoot.Value.EmailsBanList, StringComparer.OrdinalIgnoreCase);

        if (!_emailsBanList.Any())
        {
            throw new InvalidOperationException(
                message: "Please fill the emails ban list in the appsettings.json file.");
        }
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        // First use the built-in validator
        var result = await base.ValidateAsync(manager, user);
        var errors = result.Succeeded ? [] : result.Errors.ToList();

        // Extending the built-in validator
        ValidateEmail(user, errors);
        ValidateUserName(user, errors);

        return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed([.. errors]);
    }

    private void ValidateEmail(User user, List<IdentityError> errors)
    {
        var userEmail = user?.Email;

        if (string.IsNullOrWhiteSpace(userEmail))
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                errors.Add(new IdentityError
                {
                    Code = "EmailIsNotSet",
                    Description = "لطفا اطلاعات ایمیل را تکمیل کنید."
                });
            }

            return; // base.ValidateAsync() will cover this case
        }

        if (_emailsBanList.Any(email => userEmail.EndsWith(email, StringComparison.OrdinalIgnoreCase)))
        {
            errors.Add(new IdentityError
            {
                Code = "BadEmailDomainError",
                Description = "لطفا یک ایمیل پروایدر معتبر را وارد نمائید."
            });
        }
    }

    private static void ValidateUserName(User user, List<IdentityError> errors)
    {
        var userName = user?.UserName;

        if (string.IsNullOrWhiteSpace(userName))
        {
            errors.Add(new IdentityError
            {
                Code = "UserIsNotSet",
                Description = "لطفا اطلاعات کاربری را تکمیل کنید."
            });

            return; // base.ValidateAsync() will cover this case
        }

        if (userName.IsNumeric() || userName.ContainsNumber())
        {
            errors.Add(new IdentityError
            {
                Code = "BadUserNameError",
                Description = "نام کاربری وارد شده نمی‌تواند حاوی اعداد باشد."
            });
        }

        if (userName.HasConsecutiveChars())
        {
            errors.Add(new IdentityError
            {
                Code = "BadUserNameError",
                Description = "نام کاربری وارد شده معتبر نیست."
            });
        }
    }
}