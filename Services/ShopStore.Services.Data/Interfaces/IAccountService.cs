using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult> Login(string userName, string password);
        Task Logout();
        Task<OperationResult> Create(UserDTO dto);
        OperationResult Edit(UserDTO dto);
        Task<OperationResult> ChangePassword(ChangePasswordModel model);
    }
}
