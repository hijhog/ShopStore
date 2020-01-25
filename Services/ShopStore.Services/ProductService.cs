using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProductDTO Get(int id)
        {
            Product product = _unitOfWork.ProductRepository.Get(new int[] { id });
            _unitOfWork.ProductRepository.Reference(product, x => x.Category);
            return _mapper.Map<ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _unitOfWork.ProductRepository.GetAll().Include(x=>x.Category);
            return products.Select(x =>
                _mapper.Map<ProductDTO>(x));
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            try
            {
                _unitOfWork.ProductRepository.Remove(new int[] { id });
                _unitOfWork.ProductRepository.Save();

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
                Product product = _unitOfWork.ProductRepository.Get(dto.Id);
                if (product == null)
                {
                    product = _mapper.Map<Product>(dto);
                    _unitOfWork.ProductRepository.Insert(product);
                }
                else
                {
                    _mapper.Map(dto, product);
                    _unitOfWork.ProductRepository.Update(product);
                }

                _unitOfWork.ProductRepository.Save();
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
