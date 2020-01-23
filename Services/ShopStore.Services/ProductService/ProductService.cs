using ShopStore.Services.ProductService.Interfaces;
using ShopStore.Data.Entities;
using ShopStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.ProjectService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        public ProductService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
    }
}
