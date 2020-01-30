using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Data.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopStore.Data.Models.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<AppUser> UserRepository { get; }
        Task SaveAsync();
    }
}
