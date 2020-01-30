using ShopStore.Data.Contract.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopStore.Data.Models.Interfaces
{
    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        TEntity Get(params object[] id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(params object[] id);
        void Reference(TEntity entity, params Expression<Func<TEntity, object>>[] props);
        void Collection(TEntity entity, params Expression<Func<TEntity, IEnumerable<object>>>[] expression);
        IQueryable<TEntity> IncludeMultiple(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes);
    }

    public interface IRepository
    {

    }
}
