using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickRepricer.Core.Models;
using QuickRepricer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace QuickRepricer.Persistence.EntityConfigurations
{
    public class FeatureConfiguration : EntityTypeConfiguration<Feature>
    {
        public override void Map(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");
            builder.Property(f => f.Id)
              .ValueGeneratedOnAdd();

            builder.Property(f => f.Description)
                .HasMaxLength(100);

            builder.Property(f => f.CreatedBy)
                .HasMaxLength(50);

            builder.Property(f => f.ModifiedBy)
              .HasMaxLength(50);

            builder.HasOne(f => f.Plan)
                .WithMany(p => p.Features)
                .HasForeignKey(f => f.PlanId);
        }
    }
}
