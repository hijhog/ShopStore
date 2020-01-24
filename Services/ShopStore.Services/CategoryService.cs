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

        public OperationResult Save(CategoryDTO dto)
        {
            var result = new OperationResult();
            try
            {
                Category category = _categoryRepo.Get(dto.Id);
                if(category == null)
                {
                    category = _mapper.Map<Category>(dto);
                    _categoryRepo.Insert(category);
                }
                else
                {
                    _mapper.Map(dto, category);
                    _categoryRepo.Update(category);
                }

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
