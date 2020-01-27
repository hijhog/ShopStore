using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IStoreService
    {
        StoreDTO Get(Guid id);
        IEnumerable<StoreDTO> GetAll();
        OperationResult Save(StoreDTO dto);
        OperationResult Remove(Guid id);

        IEnumerable<StoreProductDTO> GetStoreProducts(Guid storeId);
        OperationResult AddProduct(StoreProductDTO dto);
        OperationResult RemoveProduct(Guid storeId, Guid prodId);
    }
}
