using ShopStore.Common;
using ShopStore.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Interfaces
{
    public interface ICategoryService
    {
        CategoryDTO Get(int id);
        IEnumerable<CategoryDTO> GetAll();
        OperationResult Save(CategoryDTO dto);
        OperationResult Remove(int id);
    }
}
