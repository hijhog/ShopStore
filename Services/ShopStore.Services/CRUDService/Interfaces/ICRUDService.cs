using ShopStore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.CRUDService.Interfaces
{
    public interface ICRUDService<TEntity,TObjectDTO> 
        where TEntity : class 
        where TObjectDTO : class
    {
        TObjectDTO Get(int id);
        IEnumerable<TObjectDTO> GetAll();
        OperationResult Add(TObjectDTO dto);
        OperationResult Edit(TObjectDTO dto, int id);
        OperationResult Remove(int id);
    }
}
