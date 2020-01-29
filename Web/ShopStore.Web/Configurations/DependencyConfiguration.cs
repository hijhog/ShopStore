using Microsoft.Extensions.DependencyInjection;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Repositories;
using ShopStore.Services;
using ShopStore.Services.Data.Interfaces;

namespace ShopStore.Web.Configurations
{
    public class DependencyConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}
