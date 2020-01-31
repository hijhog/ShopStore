using ShopStore.Common;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services.Contract.Interfaces
{
    public interface IProductService
    {
        ProductDTO Get(Guid id);
        IEnumerable<ProductDTO> GetAll();
        Task<OperationResult> SaveAsync(ProductDTO dto);
        Task<OperationResult> RemoveAsync(Guid id);

        IEnumerable<ProductDTO> GetProductsByCategory(Guid categoryId);
    }
}
