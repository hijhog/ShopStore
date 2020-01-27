using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IProductService
    {
        ProductDTO Get(Guid id);
        IEnumerable<ProductDTO> GetAll();
        OperationResult Save(ProductDTO dto);
        OperationResult Remove(Guid id);
    }
}
