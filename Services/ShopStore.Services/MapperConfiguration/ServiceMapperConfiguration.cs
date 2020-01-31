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

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
                .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.LastName} {src.User.FirstName} {src.User.Patronymic}"));
            CreateMap<OrderDTO, Order>();

            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<CartDTO, Cart>();

            CreateMap<CartDTO, Order>()
                .ForMember(dest => dest.TotalSum, opt => opt.MapFrom(src => src.Price * src.Quantity));
        }
    }
}
