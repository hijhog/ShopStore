using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> GetRoleIdByName(string name);
        IEnumerable<RoleDTO> GetRoles();
    }
}
