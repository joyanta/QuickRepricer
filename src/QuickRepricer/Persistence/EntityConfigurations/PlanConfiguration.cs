using QuickRepricer.Core.Models;
using QuickRepricer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuickRepricer.Persistence.EntityConfigurations
{
    public class PlanConfiguration : EntityTypeConfiguration<Plan>
    {
        public override void Map(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plans");

            builder.Ignore(p => p.Name);
            builder.Ignore(p => p.AmountInCents);

            builder.Ignore(p => p.Currency);

            builder.Ignore(p => p.Interval);

            builder.Ignore(p => p.TrialPeriodDays);

            builder.Property(p => p.ExternalId)
                .HasMaxLength(50);

            builder.Property(p => p.Description)
              .HasMaxLength(500);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.ModifiedBy)
              .HasMaxLength(50);

        }
    }
}
