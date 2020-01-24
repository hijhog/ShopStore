using AutoMapper;
using ShopStore.Services.CategoryService.Models;
using ShopStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ShopStore.Services.ProductService.Models;

namespace ShopStore.Services.MapperConfiguration
{
    public class ServiceMapperConfiguration : Profile
    {
        public ServiceMapperConfiguration()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}
