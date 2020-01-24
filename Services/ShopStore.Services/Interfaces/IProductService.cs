using ShopStore.Common;
using ShopStore.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Interfaces
{
    public interface IProductService
    {
        ProductDTO Get(int id);
        IEnumerable<ProductDTO> GetAll();
        OperationResult Save(ProductDTO dto);
        OperationResult Remove(int id);
    }
}
