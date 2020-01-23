using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.Stores).HasForeignKey(x => x.ProductId);
        }
    }
}
