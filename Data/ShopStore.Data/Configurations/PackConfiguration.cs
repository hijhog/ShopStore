using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStore.Data.Models.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Configurations
{
    class PackConfiguration : IEntityTypeConfiguration<Pack>
    {
        public void Configure(EntityTypeBuilder<Pack> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.Packs).HasForeignKey(x => x.ProductId);
        }
    }
}
