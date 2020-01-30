using AutoMapper;
using ShopStore.Data.Contract.BusinessEntities;
using ShopStore.Data.Models.UserEntities;
using ShopStore.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

            CreateMap<AppUser, UserDTO>();
            CreateMap<UserDTO, AppUser>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<OrderDTO, Order>();
        }
    }
}
