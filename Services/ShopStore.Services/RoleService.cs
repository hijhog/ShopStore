using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShopStore.Data.Models.UserEntities;
using ShopStore.Services.Data.Interfaces;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleDTO> GetRoleIdByName(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            return _mapper.Map<RoleDTO>(role);
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            return _roleManager.Roles.Select(x=>_mapper.Map<RoleDTO>(x));
        }
    }
}
