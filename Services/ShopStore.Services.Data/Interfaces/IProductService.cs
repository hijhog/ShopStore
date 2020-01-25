﻿using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IProductService
    {
        ProductDTO Get(int id);
        IEnumerable<ProductDTO> GetAll();
        OperationResult Save(ProductDTO dto);
        OperationResult Remove(int id);
    }
}