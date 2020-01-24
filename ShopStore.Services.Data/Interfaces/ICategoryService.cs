using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        CategoryDTO Get(int id);
        IEnumerable<CategoryDTO> GetAll();
        OperationResult Save(CategoryDTO dto);
        OperationResult Remove(int id);
    }
}
