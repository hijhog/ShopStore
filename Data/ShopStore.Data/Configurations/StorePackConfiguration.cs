using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStore.Data.Models.BusinessEntities;

namespace ShopStore.Data.Configurations
{
    public class StorePackConfiguration : IEntityTypeConfiguration<StorePack>
    {
        public void Configure(EntityTypeBuilder<StorePack> builder)
        {
            builder.HasKey(x => new { x.PackId, x.StoreId });

            builder.HasOne(x => x.Pack).WithMany(x => x.StorePacks).HasForeignKey(x => x.PackId);
            builder.HasOne(x => x.Store).WithMany(x => x.StorePacks).HasForeignKey(x => x.StoreId);
        }
    }
}
