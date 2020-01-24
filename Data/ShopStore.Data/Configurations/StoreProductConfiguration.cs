using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStore.Data.Models.BusinessEntities;

namespace ShopStore.Data.Configurations
{
    public class StoreProductConfiguration : IEntityTypeConfiguration<StoreProduct>
    {
        public void Configure(EntityTypeBuilder<StoreProduct> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.StoreId });

            builder.HasOne(x => x.Product).WithMany(x => x.StoreProducts).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Store).WithMany(x => x.StoreProducts).HasForeignKey(x => x.StoreId);
        }
    }
}
