using AutoMapper;
using ShopStore.Services.CategoryService.Models;
using ShopStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Configurations
{
    public class WebMapperConfiguration : Profile
    {
        public WebMapperConfiguration()
        {
            CreateMap<CategoryDTO, CategoryVM>();
            CreateMap<CategoryVM, CategoryDTO>();
        }
    }
}
