using ShopStore.Common;
using ShopStore.Services.Data.Models;
using System.Collections.Generic;

namespace ShopStore.Services.Data.Interfaces
{
    public interface IStoreService
    {
        StoreDTO Get(int id);
        IEnumerable<StoreDTO> GetAll();
        OperationResult Save(StoreDTO dto);
        OperationResult Remove(int id);

        IEnumerable<StoreProductDTO> GetStoreProducts(int storeId);
        OperationResult AddProduct(StoreProductDTO dto);
        OperationResult RemoveProduct(int storeId, int prodId);
    }
}
