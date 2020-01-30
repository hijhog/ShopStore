using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStore.Data.Contract.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.UserId });

            builder.HasOne(x => x.Product).WithMany(x => x.Cart).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.User).WithMany(x => x.Cart).HasForeignKey(x => x.UserId);
        }
    }
}
