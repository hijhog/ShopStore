using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopStore.Data.Configurations;
using ShopStore.Data.Models.BusinessEntities;
using ShopStore.Data.Models.UserEntities;
using System;

namespace ShopStore.Data
{
    public class ApplicationContext : IdentityDbContext<AppUser, Role, Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
