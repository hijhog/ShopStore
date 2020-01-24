using ShopStore.Data.Entities;
using ShopStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ShopStore.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopStore.Services.Interfaces;
using ShopStore.Services.Models;

namespace ShopStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        public ProductService(
            IRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public ProductDTO Get(int id)
        {
            Product product = _productRepo.Get(id);
            _productRepo.Reference(product, x => x.Category);
            return _mapper.Map<ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _productRepo.GetAll().Include(x=>x.Category);
            return products.Select(x =>
                _mapper.Map<ProductDTO>(x));
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            try
            {
                _productRepo.Remove(id);
                _productRepo.Save();

                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public OperationResult Save(ProductDTO dto)
        {
            var result = new OperationResult();
            try
            {
                Product product = _productRepo.Get(dto.Id);
                if (product == null)
                {
                    product = _mapper.Map<Product>(dto);
                    _productRepo.Insert(product);
                }
                else
                {
                    _mapper.Map(dto, product);
                    _productRepo.Update(product);
                }

                _productRepo.Save();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }
    }
}
