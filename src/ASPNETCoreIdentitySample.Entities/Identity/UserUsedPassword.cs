using ASPNETCoreIdentitySample.Entities.AuditableEntity;

namespace ASPNETCoreIdentitySample.Entities.Identity
{
    public class UserUsedPassword : IAuditableEntity
    {
        public int Id { get; set; }

        public string HashedPassword { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}