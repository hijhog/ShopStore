using Microsoft.AspNetCore.Identity;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationContext _context;
        public UserRoleRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void AddRoleToUser(Guid roleId, Guid userId)
        {
            _context.UserRoles.Add(new IdentityUserRole<Guid>
            {
                UserId = userId,
                RoleId = roleId
            });
        }

        public void RemoveRoleFromUser(Guid roleId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
