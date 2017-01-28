using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using System;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    public class UsedPasswordsService : IUsedPasswordsService
    {
        private readonly int _changePasswordReminderDays;
        private readonly int _notAllowedPreviouslyUsedPasswords;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork _uow;
        private readonly DbSet<UserUsedPassword> _userUsedPasswords;

        public UsedPasswordsService(
            IUnitOfWork uow,
            IPasswordHasher<User> passwordHasher,
            IOptionsSnapshot<SiteSettings> configurationRoot)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _userUsedPasswords = _uow.Set<UserUsedPassword>();
            _userUsedPasswords.CheckArgumentIsNull(nameof(_userUsedPasswords));

            _passwordHasher = passwordHasher;
            _passwordHasher.CheckArgumentIsNull(nameof(_passwordHasher));

            configurationRoot.CheckArgumentIsNull(nameof(configurationRoot));
            var configurationRootValue = configurationRoot.Value;
            configurationRootValue.CheckArgumentIsNull(nameof(configurationRootValue));

            _notAllowedPreviouslyUsedPasswords = configurationRootValue.NotAllowedPreviouslyUsedPasswords;
            _changePasswordReminderDays = configurationRootValue.ChangePasswordReminderDays;
        }

        public async Task AddToUsedPasswordsListAsync(User user)
        {
            await _userUsedPasswords.AddAsync(new UserUsedPassword
            {
                UserId = user.Id,
                HashedPassword = user.PasswordHash
            }).ConfigureAwait(false);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<DateTimeOffset?> GetLastUserPasswordChangeDateAsync(int userId)
        {
            var lastPasswordHistory =
                await _userUsedPasswords//.AsNoTracking() --> removes shadow properties
                                        .OrderByDescending(userUsedPassword => userUsedPassword.Id)
                                        .FirstOrDefaultAsync(userUsedPassword => userUsedPassword.UserId == userId)
                                        .ConfigureAwait(false);

            if (lastPasswordHistory == null)
            {
                return null;
            }

            var createdDateValue = _uow.GetShadowPropertyValue(lastPasswordHistory, AuditableShadowProperties.CreatedDateTime);
            return (DateTimeOffset?) createdDateValue;
        }

        public async Task<bool> IsLastUserPasswordTooOldAsync(int userId)
        {
            var createdDateTime = await GetLastUserPasswordChangeDateAsync(userId).ConfigureAwait(false);
            if (createdDateTime == null)
            {
                return false;
            }
            return createdDateTime.Value.AddDays(_changePasswordReminderDays) < DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// This method will be used by CustomPasswordValidator automatically,
        /// every time a user wants to change his/her info.
        /// </summary>
        public Task<bool> IsPreviouslyUsedPasswordAsync(User user, string newPassword)
        {
            if (user.Id == 0)
            {
                // A new user wants to register at our site
                return Task.FromResult(false);
            }

            var userId = user.Id;
            return
                _userUsedPasswords
                    .AsNoTracking()
                    .Where(userUsedPassword => userUsedPassword.UserId == userId)
                    .OrderByDescending(userUsedPassword => userUsedPassword.Id)
                    .Select(userUsedPassword => userUsedPassword.HashedPassword)
                    .Take(_notAllowedPreviouslyUsedPasswords)
                    .AnyAsync(hashedPassword => _passwordHasher.VerifyHashedPassword(user, hashedPassword, newPassword) != PasswordVerificationResult.Failed);
        }
    }
}