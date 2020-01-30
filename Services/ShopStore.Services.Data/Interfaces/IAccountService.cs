using ShopStore.Common;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services.Contract.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult> LoginAsync(string userName, string password);
        Task LogoutAsync();
        Task<OperationResult> CreateAsync(UserDTO dto);
        Task<OperationResult> EditAsync(UserDTO dto);
        Task<OperationResult> ChangePasswordAsync(ChangePasswordModel model);
    }
}
