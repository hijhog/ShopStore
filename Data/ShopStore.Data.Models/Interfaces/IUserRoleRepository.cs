using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.Interfaces
{
    public interface IUserRoleRepository
    {
        void AddRoleToUser(Guid roleId, Guid userId);
        void RemoveRoleFromUser(Guid roleId, Guid userId);
    }
}
