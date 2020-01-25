using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopStore.Data.Models.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        TEntity Get(params object[] id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(params object[] id);
        void Save();
        void Reference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> expression) where TProperty : class;
        void Collection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> expression) where TProperty : class;
    }
}
