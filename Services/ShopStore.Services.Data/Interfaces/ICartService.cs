using ShopStore.Common;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services.Contract.Interfaces
{
    public interface ICartService
    {
        IEnumerable<CartDTO> GetCartByUserId(Guid userId);
        Task<OperationResult> AddProduct(CartDTO dto);
        Task<OperationResult> RemoveProduct(Guid productId, Guid userId);
        Task<OperationResult> RemoveCartUser(Guid userId);
        int GetCountProducts(Guid userId);
        IEnumerable<Guid> GetProductIds(Guid userId);
    }
}
