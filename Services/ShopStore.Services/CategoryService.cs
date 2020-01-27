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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CategoryDTO Get(Guid id)
        {
            Category category = _unitOfWork.CategoryRepository.Get(new Guid[] { id });
            return _mapper.Map<CategoryDTO>(category);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _unitOfWork.CategoryRepository.GetAll().Select(x => 
                _mapper.Map<CategoryDTO>(x));
        }

        public OperationResult Remove(Guid id)
        {
            var result = new OperationResult();
            try
            {
                _unitOfWork.CategoryRepository.Remove(new Guid[] { id });
                _unitOfWork.Save();

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
                Category category = _unitOfWork.CategoryRepository.Get(dto.Id);
                if(category == null)
                {
                    category = _mapper.Map<Category>(dto);
                    _unitOfWork.CategoryRepository.Insert(category);
                }
                else
                {
                    _mapper.Map(dto, category);
                    _unitOfWork.CategoryRepository.Update(category);
                }

                _unitOfWork.Save();
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
