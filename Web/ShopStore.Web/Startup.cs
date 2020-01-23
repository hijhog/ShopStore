using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopStore.Services.CategoryService;
using ShopStore.Services.CategoryService.Interfaces;
using ShopStore.Data;
using ShopStore.Data.Interfaces;
using ShopStore.Data.Repositories;
using ShopStore.Web.Configurations;
using AutoMapper;
using ShopStore.Services.MapperConfiguration;
using ShopStore.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace ShopStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opt => 
                opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<AppUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAutoMapper(typeof(WebMapperConfiguration), typeof(ServiceMapperConfiguration));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Category}/{action=Index}/{id?}");
            });
        }
    }
}
