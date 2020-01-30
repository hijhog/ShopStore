using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;

        private readonly Dictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {

            if (_repositories.ContainsKey(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repositoryType = typeof(Repository<>).MakeGenericType(typeof(TEntity));
            var repository = (IRepository)Activator.CreateInstance(repositoryType, _context);
            _repositories.Add(typeof(TEntity), repository);

            return (IRepository<TEntity>)repository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
