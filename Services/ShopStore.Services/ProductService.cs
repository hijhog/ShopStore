using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopStore.Common;
using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Repositories;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _productRepository = unitOfWork.GetRepository<Product>();
            _mapper = mapper;
            _logger = logger;
        }

        public ProductDTO Get(Guid id)
        {
            Product product = _productRepository.Get(id);
            _productRepository.Reference(product, x => x.Category);
            return _mapper.Map<ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            return _productRepository.GetAll()
                .Include(x=>x.Category)
                .Select(x =>
                    _mapper.Map<ProductDTO>(x));
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var result = new OperationResult();
            try
            {
                _productRepository.Remove(id);
                await _unitOfWork.SaveAsync();

                result.Successed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to remove product";
            }
            return result;
        }

        public async Task<OperationResult> SaveAsync(ProductDTO dto)
        {
            var result = new OperationResult();
            try
            {
                Product product = _productRepository.Get(dto.Id);
                if (product == null)
                {
                    product = _mapper.Map<Product>(dto);
                    _productRepository.Insert(product);
                }
                else
                {
                    _mapper.Map(dto, product);
                    _productRepository.Update(product);
                }

                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to save product";
            }
            return result;
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(Guid categoryId)
        {
            IQueryable<Product> products;
            if (categoryId.Equals(Guid.Empty))
            {
                products = _productRepository.GetAll();
            }
            else
            {
                products = _productRepository.GetAll().Where(x => x.CategoryId == categoryId);
            }

            return products.Select(x => _mapper.Map<ProductDTO>(x));
        }

        public IEnumerable<ProductDTO> GetFilterProducts(ProductFilter filter)
        {
            return _productRepository.GetAll()
                .Where(x => 
                    (filter.Name == null || x.Name.Contains(filter.Name)) &&
                    (filter.Description == null || x.Description.Contains(filter.Description)) &&
                    (filter.Price == null || x.Price.Equals(filter.Price.Value)) &&
                    (filter.Category == null || x.Category.Name.Contains(filter.Category)))
                .Include(x => x.Category).Select(x =>
                _mapper.Map<ProductDTO>(x));
        }
    }
}
