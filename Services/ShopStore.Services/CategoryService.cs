using AutoMapper;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = unitOfWork.GetRepository<Category>();
            _mapper = mapper;
            _logger = logger;
        }

        public CategoryDTO Get(Guid id)
        {
            Category category = _categoryRepository.Get(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _categoryRepository.GetAll().Select(x => 
                _mapper.Map<CategoryDTO>(x));
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var result = new OperationResult();
            try
            {
                _categoryRepository.Remove(id);
                await _unitOfWork.SaveAsync();

                result.Successed = true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to remove category";
            }
            return result;
        }

        public async Task<OperationResult> SaveAsync(CategoryDTO dto)
        {
            var result = new OperationResult();
            try
            {
                Category category = _categoryRepository.Get(dto.Id);
                if(category == null)
                {
                    category = _mapper.Map<Category>(dto);
                    _categoryRepository.Insert(category);
                }
                else
                {
                    _mapper.Map(dto, category);
                    _categoryRepository.Update(category);
                }

                await _unitOfWork.SaveAsync();
                result.Successed = true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception: {ex.GetType().ToString()}; Message: {ex.Message}; StackTrace: {ex.StackTrace}");
                result.Description = "Failed to save category";
            }
            return result;
        }
    }
}
