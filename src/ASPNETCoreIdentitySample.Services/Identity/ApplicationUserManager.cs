using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2578
/// </summary>
public class ApplicationUserManager(
    IApplicationUserStore store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<User> passwordHasher,
    IEnumerable<IUserValidator<User>> userValidators,
    IEnumerable<IPasswordValidator<User>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<ApplicationUserManager> logger,
    IHttpContextAccessor contextAccessor,
    IUnitOfWork uow,
    IUsedPasswordsService usedPasswordsService) : UserManager<User>(
        (UserStore<User, Role, ApplicationDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>)store,
        optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger),
    IApplicationUserManager
{
    private readonly IHttpContextAccessor _contextAccessor =
        contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));

    private readonly DbSet<Role> _roles = uow.Set<Role>();

    private readonly IUsedPasswordsService _usedPasswordsService =
        usedPasswordsService ?? throw new ArgumentNullException(nameof(usedPasswordsService));

    private readonly DbSet<User> _users = uow.Set<User>();
    private User _currentUserInScope;

    #region BaseClass

    string IApplicationUserManager.CreateTwoFactorRecoveryCode() => base.CreateTwoFactorRecoveryCode();

    Task<PasswordVerificationResult> IApplicationUserManager.VerifyPasswordAsync(IUserPasswordStore<User> store,
        User user,
        string password)
        => base.VerifyPasswordAsync(store, user, password);

    public override async Task<IdentityResult> CreateAsync(User user)
    {
        var result = await base.CreateAsync(user);

        if (result.Succeeded)
        {
            // Skip adding to used passwords when there is no local password (external login scenario)
            if (!string.IsNullOrWhiteSpace(user?.PasswordHash))
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
        }

        return result;
    }

    public override Task<IdentityResult> CreateAsync(User user, string password) => base.CreateAsync(user, password);

    public override async Task<IdentityResult> ChangePasswordAsync(User user,
        string currentPassword,
        string newPassword)
    {
        var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result.Succeeded)
        {
            await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
        }

        return result;
    }

    public override async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        var result = await base.ResetPasswordAsync(user, token, newPassword);

        if (result.Succeeded)
        {
            await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
        }

        return result;
    }

    #endregion

    #region CustomMethods

    public User FindById(int userId) => _users.Find(userId);

    public Task<User> FindByIdIncludeUserRolesAsync(int userId)
        => _users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == userId);

    public Task<List<User>> GetAllUsersAsync() => Users.ToListAsync();

    public User GetCurrentUser()
    {
        if (_currentUserInScope != null)
        {
            return _currentUserInScope;
        }

        var currentUserId = GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return null;
        }

        var userId = int.Parse(currentUserId, NumberStyles.Number, CultureInfo.InvariantCulture);

        return _currentUserInScope = FindById(userId);
    }

    public async Task<User> GetCurrentUserAsync()
    {
        if (_contextAccessor.HttpContext is null)
        {
            return null;
        }

        return _currentUserInScope ??= await GetUserAsync(_contextAccessor.HttpContext.User);
    }

    public string GetCurrentUserId() => _contextAccessor.HttpContext?.User.Identity?.GetUserId();

    public int? GetCurrentIntUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.Identity?.GetUserId();

        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }

        return !int.TryParse(userId, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? null : result;
    }

    IPasswordHasher<User> IApplicationUserManager.PasswordHasher
    {
        get => base.PasswordHasher;
        set => base.PasswordHasher = value;
    }

    IList<IUserValidator<User>> IApplicationUserManager.UserValidators => base.UserValidators;

    IList<IPasswordValidator<User>> IApplicationUserManager.PasswordValidators => base.PasswordValidators;

    IQueryable<User> IApplicationUserManager.Users => base.Users;

    public string GetCurrentUserName() => _contextAccessor.HttpContext?.User.Identity?.GetUserName();

    public async Task<bool> HasPasswordAsync(int userId)
    {
        var user = await FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));

        return user?.PasswordHash != null;
    }

    public async Task<bool> HasPhoneNumberAsync(int userId)
    {
        var user = await FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));

        return user?.PhoneNumber != null;
    }

    public async Task<byte[]> GetEmailImageAsync(int? userId)
    {
        if (userId == null)
        {
            return "?".TextToImage(new TextToImageOptions());
        }

        var user = await FindByIdAsync(userId.Value.ToString(CultureInfo.InvariantCulture));

        if (user == null)
        {
            return "?".TextToImage(new TextToImageOptions());
        }

        if (!user.IsEmailPublic)
        {
            return "?".TextToImage(new TextToImageOptions());
        }

        return user.Email.TextToImage(new TextToImageOptions());
    }

    public async Task<PagedUsersListViewModel> GetPagedUsersListAsync(SearchUsersViewModel model, int pageNumber)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var skipRecords = pageNumber * model.MaxNumberOfRows;
        var query = _users.Include(x => x.Roles).AsNoTracking();

        if (!model.ShowAllUsers)
        {
            query = query.Where(x => x.IsActive == model.UserIsActive);
        }

        if (!string.IsNullOrWhiteSpace(model.TextToFind))
        {
            model.TextToFind = model.TextToFind.ApplyCorrectYeKe();

            if (model.IsPartOfEmail)
            {
                query = query.Where(x => x.Email.Contains(model.TextToFind));
            }

            if (model.IsUserId)
            {
                if (int.TryParse(model.TextToFind, NumberStyles.Number, CultureInfo.InvariantCulture, out var userId))
                {
                    query = query.Where(x => x.Id == userId);
                }
            }

            if (model.IsPartOfName)
            {
                query = query.Where(x => x.FirstName.Contains(model.TextToFind));
            }

            if (model.IsPartOfLastName)
            {
                query = query.Where(x => x.LastName.Contains(model.TextToFind));
            }

            if (model.IsPartOfUserName)
            {
                query = query.Where(x => x.UserName.Contains(model.TextToFind));
            }

            if (model.IsPartOfLocation)
            {
                query = query.Where(x => x.Location.Contains(model.TextToFind));
            }
        }

        if (model.HasEmailConfirmed)
        {
            query = query.Where(x => x.EmailConfirmed);
        }

        if (model.UserIsLockedOut)
        {
            query = query.Where(x => x.LockoutEnd != null);
        }

        if (model.HasTwoFactorEnabled)
        {
            query = query.Where(x => x.TwoFactorEnabled);
        }

        query = query.OrderBy(x => x.Id);

        return new PagedUsersListViewModel
        {
            Paging =
            {
                TotalItems = await query.CountAsync()
            },
            Users = await query.Skip(skipRecords).Take(model.MaxNumberOfRows).ToListAsync(),
            Roles = await _roles.ToListAsync()
        };
    }

    public async Task<PagedUsersListViewModel> GetPagedUsersListAsync(int pageNumber,
        int recordsPerPage,
        string sortByField,
        SortOrder sortOrder,
        bool showAllUsers)
    {
        var skipRecords = pageNumber * recordsPerPage;
        var query = _users.Include(x => x.Roles).AsNoTracking();

        if (!showAllUsers)
        {
            query = query.Where(x => x.IsActive);
        }

        query = sortOrder == SortOrder.Descending ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);

        return new PagedUsersListViewModel
        {
            Paging =
            {
                TotalItems = await query.CountAsync()
            },
            Users = await query.Skip(skipRecords).Take(recordsPerPage).ToListAsync(),
            Roles = await _roles.ToListAsync()
        };
    }

    public async Task<IdentityResult> UpdateUserAndSecurityStampAsync(int userId, Action<User> action)
    {
        var user = await FindByIdIncludeUserRolesAsync(userId);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "کاربر مورد نظر یافت نشد."
            });
        }

        action?.Invoke(user);

        var result = await UpdateAsync(user);

        if (!result.Succeeded)
        {
            return result;
        }

        return await UpdateSecurityStampAsync(user);
    }

    public async Task<IdentityResult> AddOrUpdateUserRolesAsync(int userId,
        IList<int> selectedRoleIds,
        Action<User> action = null)
    {
        var user = await FindByIdIncludeUserRolesAsync(userId);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "کاربر مورد نظر یافت نشد."
            });
        }

        var currentUserRoleIds = user.Roles.Select(x => x.RoleId).ToList();

        selectedRoleIds ??= [];

        var newRolesToAdd = selectedRoleIds.Except(currentUserRoleIds).ToList();

        foreach (var roleId in newRolesToAdd)
        {
            user.Roles.Add(new UserRole
            {
                RoleId = roleId,
                UserId = user.Id
            });
        }

        var removedRoles = currentUserRoleIds.Except(selectedRoleIds).ToList();

        foreach (var roleId in removedRoles)
        {
            var userRole = user.Roles.SingleOrDefault(ur => ur.RoleId == roleId);

            if (userRole != null)
            {
                user.Roles.Remove(userRole);
            }
        }

        action?.Invoke(user);

        var result = await UpdateAsync(user);

        if (!result.Succeeded)
        {
            return result;
        }

        return await UpdateSecurityStampAsync(user);
    }

    Task<IdentityResult> IApplicationUserManager.UpdatePasswordHash(User user,
        string newPassword,
        bool validatePassword)
        => base.UpdatePasswordHash(user, newPassword, validatePassword);

    #endregion
}