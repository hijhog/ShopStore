using Microsoft.Extensions.DependencyInjection;
using ShopStore.Data.Interfaces;
using ShopStore.Data.Repositories;
using ShopStore.Services.CategoryService;
using ShopStore.Services.CategoryService.Interfaces;
using ShopStore.Services.ProductService.Interfaces;
using ShopStore.Services.ProjectService;

namespace ShopStore.Web.Configurations
{
    public class DependencyConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
