using AutoMapper;
using ShopStore.Services.Data.Models;
using ShopStore.Web.Areas.Admin.Models;

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

            CreateMap<StoreProductDTO, StoreProductVM>();
            CreateMap<StoreProductVM, StoreProductDTO>();
        }
    }
}
