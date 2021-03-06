﻿using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Data.Models.Interfaces;
using ShopStore.Services;
using ShopStore.Services.Contract.Interfaces;
using ShopStore.Services.Contract.Models;
using ShopStore.Services.MapperConfiguration;
using ShopStore.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShopStore.Tests
{
    public class ProductServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRepository<Product>> _mockProductRepository;
        private Mock<IRepository<Category>> _mockCategoryRepository;
        private IProductService _productService;
        private IMapper _mapper;
        private List<Guid> categoryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        public ProductServiceTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapperConfiguration());
                cfg.AddProfile(new WebMapperConfiguration());
            });
            _mapper = new Mapper(configuration);
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockCategoryRepository = new Mock<IRepository<Category>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository.Setup(x => x.GetAll()).Returns(GetListProducts());
            _mockCategoryRepository.Setup(x => x.GetAll()).Returns(GetListCategories());
            _mockUnitOfWork.Setup(x => x.GetRepository<Product>()).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(x => x.GetRepository<Category>()).Returns(_mockCategoryRepository.Object);
            _productService = new ProductService(_mockUnitOfWork.Object, _mapper, new NullLogger<ProductService>());
        }

        [Fact]
        public void GetAllReturnsListOfProducts()
        {
            //Act
            var list = _productService.GetAll();

            //Assert
            Assert.Equal(GetListProducts().ToList().Count, list.Count());
        }

        [Fact]
        public void CreatingNewProduct()
        {
            //Arrange
            var productDTO = new ProductDTO
            {
                Id = Guid.NewGuid(),
                Name = "Product",
                Description = "New Product",
                Price = 14.50M,
                CategoryId = categoryIds[0]
            };

            //Act
            var result = _productService.SaveAsync(productDTO).Result;

            //Assert
            Assert.True(result.Successed);
        }

        [Fact]
        public void EditingExistProduct()
        {
            //Arrange
            var product = GetListProducts().First();
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = "Banana",
                Description = "Banana",
                Price = 13.50M,
                CategoryId = product.CategoryId
            };

            //Act
            var result = _productService.SaveAsync(productDTO).Result;

            //Assert
            Assert.True(result.Successed);
        }

        [Fact]
        public void RemovingProduct()
        {
            //Arrange
            var productId = GetListProducts().First().Id;

            //Act
            var result = _productService.RemoveAsync(productId).Result;

            //Assert
            Assert.True(result.Successed);
        }

        [Fact]
        public void ReturnsOnlyProductsByCategory()
        {
            //Assign
            var categoryId = categoryIds[0];

            //Act
            var list = _productService.GetProductsByCategory(categoryId);

            //Assert
            Assert.Equal(GetListProducts().Where(x => x.CategoryId == categoryId).Count(), list.Count());
        }

        [Fact]
        public void TestProductFilter()
        {
            //Assign
            var nameFilter = new ProductFilter { Name = "Puma" };
            var descFilter = new ProductFilter { Description = "very" };
            var priceFilter = new ProductFilter { Price = 3.99M };
            var categoryFilter = new ProductFilter { Category = "shirt" };
            var descCategoryFilter = new ProductFilter { Description = "very", Category = "Shoes" };

            //Act
            var nameList = _productService.GetFilterProducts(nameFilter);
            var descList = _productService.GetFilterProducts(descFilter);
            var priceList = _productService.GetFilterProducts(priceFilter);
            var categoryList = _productService.GetFilterProducts(categoryFilter);
            var descCategoryList = _productService.GetFilterProducts(descCategoryFilter);

            //Assert
            Assert.True(nameList.All(x => x.Name.Contains("Puma")));
            Assert.True(descList.All(x => x.Description.Contains("very")));
            Assert.True(priceList.All(x => x.Price.Equals(3.99M)));
            Assert.True(categoryList.All(x => x.Category.Contains("shirt")));
            Assert.True(descCategoryList.All(x => x.Description.Contains("very") && x.Category.Contains("Shoes")));

        }

        private IQueryable<Product> GetListProducts()
        {
            var categories = GetListCategories().ToList();
            var products = new List<Product>
            {
                new Product{Id=Guid.NewGuid(),Name="Adidas",Description="The best shoes",Price=15.99M,CategoryId=categories[0].Id, Category = categories[0]},
                new Product{Id=Guid.NewGuid(),Name="Puma",Description="The very good shoes",Price=90M,CategoryId=categories[0].Id, Category = categories[0]},
                new Product{Id=Guid.NewGuid(),Name="Lining",Description="The very handsome shoes",Price=3.99M,CategoryId=categories[0].Id, Category = categories[0]},
                new Product{Id=Guid.NewGuid(),Name="Nike",Description="The very ugly shirt",Price=1.55M,CategoryId=categories[1].Id, Category = categories[1]},
            };
            return products.AsQueryable();
        }
        private IQueryable<Category> GetListCategories()
        {
            var categories = new List<Category>
            {
                new Category{Id=categoryIds[0],Name="Shoes"},
                new Category{Id=categoryIds[1],Name="Shirts"},
            };
            return categories.AsQueryable();
        }
    }
}
