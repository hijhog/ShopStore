using AutoMapper;
using ShopStore.Common;
using ShopStore.Services.CategoryService.Interfaces;
using ShopStore.Services.CategoryService.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ShopStore.Data.Interfaces;
using ShopStore.Data.Entities;

namespace ShopStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(
            IRepository<Category> categoryRepo,
            IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public CategoryDTO Get(int id)
        {
            Category category = _categoryRepo.Get(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _categoryRepo.GetAll().Select(x => 
                _mapper.Map<CategoryDTO>(x));
        }

        public OperationResult Add(CategoryDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var category = _mapper.Map<Category>(dto);
                _categoryRepo.Insert(category);
                _categoryRepo.Save();

                result.Successed = true;

            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public OperationResult Edit(CategoryDTO dto)
        {
            var result = new OperationResult();
            try
            {
                Category category = _categoryRepo.Get(dto.Id);
                _mapper.Map(dto, category);
                _categoryRepo.Update(category);
                _categoryRepo.Save();

                result.Successed = true;
            }
            catch(Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            try
            {
                _categoryRepo.Remove(id);
                _categoryRepo.Save();

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
