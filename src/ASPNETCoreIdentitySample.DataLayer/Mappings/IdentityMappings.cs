using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.DataLayer.Mappings
{
    public static class IdentityMappings
    {
        /// <summary>
        /// Adds all of the ASP.NET Core Identity related mappings at once.
        /// More info: http://www.dotnettips.info/post/2577
        /// and http://www.dotnettips.info/post/2578
        /// </summary>
        public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder, SiteSettings siteSettings)
        {
            modelBuilder.Entity<RoleClaim>(builder =>
            {
                builder.HasOne(roleClaim => roleClaim.Role).WithMany(role => role.Claims).HasForeignKey(roleClaim => roleClaim.RoleId);
                builder.ToTable("AppRoleClaims");
            });

            modelBuilder.Entity<Role>(builder =>
            {
                builder.ToTable("AppRoles");
            });

            modelBuilder.Entity<UserClaim>(builder =>
            {
                builder.HasOne(userClaim => userClaim.User).WithMany(user => user.Claims).HasForeignKey(userClaim => userClaim.UserId);
                builder.ToTable("AppUserClaims");
            });

            modelBuilder.Entity<UserLogin>(builder =>
            {
                builder.HasOne(userLogin => userLogin.User).WithMany(user => user.Logins).HasForeignKey(userLogin => userLogin.UserId);
                builder.ToTable("AppUserLogins");
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("AppUsers");
            });

            modelBuilder.Entity<UserRole>(builder =>
            {
                builder.HasOne(userRole => userRole.Role).WithMany(role => role.Users).HasForeignKey(userRole => userRole.RoleId);
                builder.HasOne(userRole => userRole.User).WithMany(user => user.Roles).HasForeignKey(userRole => userRole.UserId);
                builder.ToTable("AppUserRoles");
            });

            modelBuilder.Entity<UserToken>(builder =>
            {
                builder.HasOne(userToken => userToken.User).WithMany(user => user.UserTokens).HasForeignKey(userToken => userToken.UserId);
                builder.ToTable("AppUserTokens");
            });

            modelBuilder.Entity<UserUsedPassword>(builder =>
            {
                builder.ToTable("AppUserUsedPasswords");
                builder.Property(applicationUserUsedPassword => applicationUserUsedPassword.HashedPassword)
                    .HasMaxLength(450)
                    .IsRequired();
                builder.HasOne(applicationUserUsedPassword => applicationUserUsedPassword.User)
                    .WithMany(applicationUser => applicationUser.UserUsedPasswords);
            });

            modelBuilder.Entity<AppSqlCache>(builder =>
            {
                // For Microsoft.Extensions.Caching.SqlServer
                var cacheOptions = siteSettings.CookieOptions.DistributedSqlServerCacheOptions;
                builder.ToTable(cacheOptions.TableName, cacheOptions.SchemaName);
                builder.HasIndex(e => e.ExpiresAtTime).HasName("Index_ExpiresAtTime");
                builder.Property(e => e.Id).HasMaxLength(449);
                builder.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<AppDataProtectionKey>(builder =>
            {
                builder.ToTable("AppDataProtectionKeys");
                builder.HasIndex(e => e.FriendlyName).IsUnique();
            });
        }
    }
}