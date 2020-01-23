using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopStore.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
        void Save();
    }
}
