using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentitySample.Entities.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2577
///     and http://www.dntips.ir/post/2578
///     plus http://www.dntips.ir/post/2559
/// </summary>
public class User : IdentityUser<int>, IAuditableEntity
{
    public User()
    {
        UserUsedPasswords = [];
        UserTokens = [];
    }

    [StringLength(maximumLength: 450)] public string FirstName { get; set; }

    [StringLength(maximumLength: 450)] public string LastName { get; set; }

    [NotMapped]
    public string DisplayName
    {
        get
        {
            var displayName = $"{FirstName} {LastName}";

            return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
        }
    }

    [StringLength(maximumLength: 450)] public string PhotoFileName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? LastVisitDateTime { get; set; }

    public bool IsEmailPublic { get; set; }

    public string Location { set; get; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }

    public virtual ICollection<UserToken> UserTokens { get; set; }

    public virtual ICollection<UserRole> Roles { get; set; }

    public virtual ICollection<UserLogin> Logins { get; set; }

    public virtual ICollection<UserClaim> Claims { get; set; }
}