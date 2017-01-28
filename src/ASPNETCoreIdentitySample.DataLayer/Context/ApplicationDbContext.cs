using ASPNETCoreIdentitySample.DataLayer.Mappings;
using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.DataLayer.Context
{
    public class ApplicationDbContext : ApplicationDbContextBase
    {
        public ApplicationDbContext(
            IOptionsSnapshot<SiteSettings> siteSettings,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            ILogger<ApplicationDbContextBase> logger)
            : base(siteSettings, httpContextAccessor, hostingEnvironment, logger)
        {
        }

        public virtual DbSet<Category> Categories { set; get; }
        public virtual DbSet<Product> Products { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // Adds all of the ASP.NET Core Identity related mappings at once.
            builder.AddCustomIdentityMappings(SiteSettings.Value);

            // Custom application mappings
            builder.Entity<Category>(build =>
            {
                build.Property(category => category.Name).HasMaxLength(450).IsRequired();
                build.Property(category => category.Title).IsRequired();
            });

            builder.Entity<Product>(build =>
            {
                build.Property(product => product.Name).HasMaxLength(450).IsRequired();
                build.HasOne(product => product.Category)
                       .WithMany(category => category.Products);
            });


            // This should be placed here, at the end.
            builder.AddAuditableShadowProperties();
        }
    }
}