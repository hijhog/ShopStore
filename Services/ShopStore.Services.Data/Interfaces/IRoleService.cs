using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopStore.Services.Contract.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> GetRoleIdByNameAsync(string name);
        IEnumerable<RoleDTO> GetRoles();
    }
}
