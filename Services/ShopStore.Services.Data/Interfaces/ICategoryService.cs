using ShopStore.Common;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Services.Contract.Interfaces
{
    public interface ICategoryService
    {
        CategoryDTO Get(Guid id);
        IEnumerable<CategoryDTO> GetAll();
        Task<OperationResult> SaveAsync(CategoryDTO dto);
        Task<OperationResult> RemoveAsync(Guid id);
    }
}
