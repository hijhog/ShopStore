﻿using ShopStore.Data.Models.BusinessEntities;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Store> StoreRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<StoreProduct> StoreProductRepository { get; }
        IRepository<AppUser> UserRepository { get; }
    }
}
