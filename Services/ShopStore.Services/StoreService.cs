using ShopStore.Data.Models.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Services.Data.Interfaces;

namespace ShopStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> _storeRepo;
        public StoreService(
            IRepository<Store> storeRepo)
        {
            _storeRepo = storeRepo;
        }

        //public ProductDTO Get(int id)
        //{
        //    Product product = _productRepo.Get(id);
        //    _productRepo.Reference(product, x => x.Category);
        //    return _mapper.Map<ProductDTO>(product);
        //}

        //public IEnumerable<ProductDTO> GetAll()
        //{
        //    var products = _productRepo.GetAll().Include(x => x.Category);
        //    return products.Select(x =>
        //        _mapper.Map<ProductDTO>(x));
        //}

        //public OperationResult Remove(int id)
        //{
        //    var result = new OperationResult();
        //    try
        //    {
        //        _productRepo.Remove(id);
        //        _productRepo.Save();

        //        result.Successed = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Description = ex.Message;
        //    }
        //    return result;
        //}

        //public OperationResult Save(ProductDTO dto)
        //{
        //    var result = new OperationResult();
        //    try
        //    {
        //        Product product = _productRepo.Get(dto.Id);
        //        if (product == null)
        //        {
        //            product = _mapper.Map<Product>(dto);
        //            _productRepo.Insert(product);
        //        }
        //        else
        //        {
        //            _mapper.Map(dto, product);
        //            _productRepo.Update(product);
        //        }

        //        _productRepo.Save();
        //        result.Successed = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Description = ex.Message;
        //    }
        //    return result;
        //}
    }
}
