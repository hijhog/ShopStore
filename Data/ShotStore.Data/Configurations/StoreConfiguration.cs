using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShotStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShotStore.Data.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.Stores).HasForeignKey(x => x.ProductId);
        }
    }
}
