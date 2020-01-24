﻿using AutoMapper;
using ShopStore.Services.Data.Models;
using ShopStore.Web.Models;

namespace ShopStore.Web.Configurations
{
    public class WebMapperConfiguration : Profile
    {
        public WebMapperConfiguration()
        {
            CreateMap<CategoryDTO, CategoryVM>();
            CreateMap<CategoryVM, CategoryDTO>();

            CreateMap<ProductDTO, ProductVM>();
            CreateMap<ProductVM, ProductDTO>();

            CreateMap<StoreDTO, StoreVM>();
            CreateMap<StoreVM, StoreDTO>();
        }
    }
}
