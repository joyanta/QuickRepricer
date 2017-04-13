using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickRepricer.Core.Models;
using QuickRepricer.Persistence.Extensions;


namespace QuickRepricer.Persistence.EntityConfigurations
{
    public class MarketPlaceConfiguration : EntityTypeConfiguration<MarketPlace>
    {
        public override void Map(EntityTypeBuilder<MarketPlace> builder)
        {
            builder.ToTable("MarketPlaces");
            builder.HasOne(mp => mp.Merchant)
                .WithMany(m => m.MarketPlaces);
        }
    }
}