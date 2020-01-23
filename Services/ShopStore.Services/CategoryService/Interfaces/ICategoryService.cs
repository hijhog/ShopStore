using ShopStore.Common;
using ShopStore.Services.CategoryService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.CategoryService.Interfaces
{
    public interface ICategoryService
    {
        CategoryDTO Get(int id);
        IEnumerable<CategoryDTO> GetAll();
        OperationResult Add(CategoryDTO dto);
        OperationResult Edit(CategoryDTO dto);
        OperationResult Remove(int id);
    }
}
