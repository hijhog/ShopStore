using AutoMapper;
using ShopStore.Services.Contract.Models;
using ShopStore.Web.Areas.Admin.Models;
using ShopStore.Web.Models;

namespace ShopStore.Web.Configurations
{
    public class WebMapperConfiguration : Profile
    {
        public WebMapperConfiguration()
        {
            CreateMap<CategoryDTO, CategoryVM>();
            CreateMap<CategoryVM, CategoryDTO>();

            CreateMap<ProductDTO, ProductVM>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<ProductVM, ProductDTO>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<UserDTO, UserVM>();
            CreateMap<UserVM, UserDTO>();
        }
    }
}
