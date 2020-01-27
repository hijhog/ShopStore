using Microsoft.EntityFrameworkCore;
using ShopStore.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopStore.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _table;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public TEntity Get(params object[] id)
        {
            return _table.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _table;
        }

        public void Insert(TEntity entity)
        {
            _table.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(params object[] id)
        {
            TEntity entity = _table.Find(id);
            _table.Remove(entity);
        }

        public void Reference<TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, TProperty>> expression)
            where TProperty : class
        {
            _context.Entry(entity).Reference(expression).Load();
        }

        public void Collection<TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, IEnumerable<TProperty>>> expression) where TProperty : class
        {
            _context.Entry(entity).Collection(expression).Load();
        }
    }
}
