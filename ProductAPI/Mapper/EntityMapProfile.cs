using AutoMapper;
using ProductAPI.Entities;
using ProductAPI.Services.Categories.Dto;
using ProductAPI.Services.Products.Dto;

namespace ProductAPI.Mapper
{
    public class EntityMapProfile : Profile
    {
        public EntityMapProfile() {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
