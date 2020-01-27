using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopStore.Common;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Data.Seeding
{
    public class UserSeeder
    {
        private const string AdminLogin = "admin";
        private const string AdminEmail = "test@gmail.com";
        private const string Password = "1qaz@WSX";

        public async Task SeedAsync(ApplicationContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            await SeedUserAsync(userManager, context);
        }

        private static async Task SeedUserAsync(
            UserManager<AppUser> userManager, ApplicationContext context)
        {
            var user = await userManager.FindByNameAsync(AdminLogin);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = AdminLogin,
                    LastName = "Главный",
                    FirstName = "Админ",
                    Patronymic = "Системы",
                    Email = AdminEmail,
                };
                var result = await userManager.CreateAsync(user, Password);
                if (!result.Succeeded)
                {
                    return;
                }
                else
                {
                    var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == GlobalConstants.Admin);
                    if (role != null)
                    {
                        context.UserRoles.Add(new IdentityUserRole<Guid>
                        {
                            UserId = user.Id,
                            RoleId = role.Id,
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
