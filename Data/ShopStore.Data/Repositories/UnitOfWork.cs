using ShopStore.Data.Models.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            ProductRepository = new Repository<Product>(_context);
            CategoryRepository = new Repository<Category>(_context);
            PackRepository = new Repository<Pack>(_context);
            StoreRepository = new Repository<Store>(_context);
            OrderRepository = new Repository<Order>(_context);
            StorePackRepository = new Repository<StorePack>(_context);
            UserRepository = new Repository<AppUser>(_context);
        }

        public IRepository<Product> ProductRepository { get; }

        public IRepository<Category> CategoryRepository { get; }

        public IRepository<Pack> PackRepository { get; }

        public IRepository<Store> StoreRepository { get; }

        public IRepository<Order> OrderRepository { get; }

        public IRepository<StorePack> StorePackRepository { get; }

        public IRepository<AppUser> UserRepository { get; }
    }
}
