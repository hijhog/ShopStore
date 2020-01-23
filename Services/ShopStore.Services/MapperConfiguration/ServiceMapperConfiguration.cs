using AutoMapper;
using ShopStore.Services.CategoryService.Models;
using ShopStore.Data.Entities;
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
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
