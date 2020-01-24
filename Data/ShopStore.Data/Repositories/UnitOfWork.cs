using ShopStore.Data.Models.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Product> _productRepository = null;
        private readonly IRepository<Category> _categoryRepository = null;
        private readonly IRepository<Pack> _packRepository = null;
        private readonly IRepository<Store> _storeRepository = null;
        private readonly IRepository<Order> _orderRepository = null;
        private readonly IRepository<StorePack> _storePackRepository = null;
        private readonly IRepository<AppUser> _userRepository = null;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository ?? new Repository<Product>(_context); }
        }

        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? new Repository<Category>(_context); }
        }

        public IRepository<Pack> PackRepository
        {
            get { return _packRepository ?? new Repository<Pack>(_context); }
        }

        public IRepository<Store> StoreRepository
        {
            get { return _storeRepository ?? new Repository<Store>(_context); }
        }

        public IRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? new Repository<Order>(_context); }
        }

        public IRepository<StorePack> StorePackRepository
        {
            get { return _storePackRepository ?? new Repository<StorePack>(_context); }
        }

        public IRepository<AppUser> UserRepository
        {
            get { return _userRepository ?? new Repository<AppUser>(_context); }
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
