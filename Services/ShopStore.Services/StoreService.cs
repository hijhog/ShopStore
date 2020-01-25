using AutoMapper;
using ShopStore.Common;
using ShopStore.Data.Models.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Services.Data.Interfaces;
using ShopStore.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StoreService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public StoreDTO Get(int id)
        {
            Store store = _unitOfWork.StoreRepository.Get(id);
            return _mapper.Map<StoreDTO>(store);
        }

        public IEnumerable<StoreDTO> GetAll()
        {
            return _unitOfWork.StoreRepository.GetAll().Select(x =>
                _mapper.Map<StoreDTO>(x));
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            try
            {
                _unitOfWork.StoreRepository.Remove(id);
                _unitOfWork.StoreRepository.Save();

                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public OperationResult Save(StoreDTO dto)
        {
            var result = new OperationResult();
            try
            {
                Store store = _unitOfWork.StoreRepository.Get(dto.Id);
                if (store == null)
                {
                    store = _mapper.Map<Store>(dto);
                    _unitOfWork.StoreRepository.Insert(store);
                }
                else
                {
                    _mapper.Map(dto, store);
                    _unitOfWork.StoreRepository.Update(store);
                }

                _unitOfWork.StoreRepository.Save();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public IEnumerable<StoreProductDTO> GetStoreProducts(int storeId)
        {
            var storeProducts = from product in _unitOfWork.ProductRepository.GetAll()
                                join category in _unitOfWork.CategoryRepository.GetAll()
                                on product.CategoryId equals category.Id
                                join sp in _unitOfWork.StoreProductRepository.GetAll()
                                on product.Id equals sp.ProductId into g
                                from storeProd in g.DefaultIfEmpty()
                                select new StoreProductDTO
                                {
                                    StoreId = storeId,
                                    ProductId = product.Id,
                                    Name = product.Name,
                                    Description = product.Description,
                                    Price = product.Price,
                                    Category = category.Name,
                                    ProductCount = storeProd == null ? 0 : storeProd.ProductCount,
                                    IsExistInStore = storeProd != null
                                };
            return storeProducts;
        }

        public OperationResult AddProduct(StoreProductDTO dto)
        {
            var result = new OperationResult();
            try
            {
                StoreProduct storeProduct = _unitOfWork.StoreProductRepository.Get(dto.ProductId, dto.StoreId);
                if(storeProduct == null)
                {
                    storeProduct = _mapper.Map<StoreProduct>(dto);
                    _unitOfWork.StoreProductRepository.Insert(storeProduct);
                }
                else
                {
                    _mapper.Map(dto, storeProduct);
                    _unitOfWork.StoreProductRepository.Update(storeProduct);
                }
                _unitOfWork.StoreProductRepository.Save();
                result.Successed = true;
            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public OperationResult RemoveProduct(int storeId, int prodId)
        {
            var result = new OperationResult();
            try
            {
                _unitOfWork.StoreProductRepository.Remove(storeId, prodId);
                _unitOfWork.StoreProductRepository.Save();
                result.Successed = true;
            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }
    }
}
