using Microsoft.EntityFrameworkCore;
using ShotStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShotStore.Data.Repositories
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

        public TEntity Get(int id)
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

        public void Remove(int id)
        {
            TEntity entity = _table.Find(id);
            _table.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
