using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickRepricer.Core.Models;
using QuickRepricer.Persistence.EntityConfigurations;
using QuickRepricer.Persistence.Extensions;

namespace QuickRepricer.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Feature> Features { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<MarketPlace> MarketPlaces { get; set; }

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);



            //ApplicationUser
            builder.AddConfiguration(new ApplicationUserConfiguration());

            //Plan;
            builder.AddConfiguration(new PlanConfiguration());

            //Feature
            builder.AddConfiguration(new FeatureConfiguration());

            //MarketPlace
            builder.AddConfiguration(new MarketPlaceConfiguration());
        }
    }
}
