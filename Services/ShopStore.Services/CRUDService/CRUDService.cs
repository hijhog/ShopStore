using AutoMapper;
using ShopStore.Common;
using ShopStore.Data.Interfaces;
using ShopStore.Services.CRUDService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopStore.Services.CRUDService
{
    public class CRUDService<TEntity, TObjectDTO> : ICRUDService<TEntity, TObjectDTO>
        where TEntity : class
        where TObjectDTO : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        public CRUDService(
            IRepository<TEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public OperationResult Add(TObjectDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var category = _mapper.Map<TEntity>(dto);
                _repository.Insert(category);
                _repository.Save();

                result.Successed = true;

            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public OperationResult Edit(TObjectDTO dto, int id)
        {
            var result = new OperationResult();
            try
            {
                TEntity category = _repository.Get(id);
                _mapper.Map(dto, category);
                _repository.Update(category);
                _repository.Save();

                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
            }
            return result;
        }

        public TObjectDTO Get(int id)
        {
            TEntity category = _repository.Get(id);
            return _mapper.Map<TObjectDTO>(category);
        }

        public IEnumerable<TObjectDTO> GetAll()
        {
            return _repository.GetAll().Select(x =>
                _mapper.Map<TObjectDTO>(x));
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            try
            {
                _repository.Remove(id);
                _repository.Save();

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
