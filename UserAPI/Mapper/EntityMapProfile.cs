using AutoMapper;
using UserAPI.Entities;
using UserAPI.Services.Dto;

namespace UserAPI.Mapper
{
    public class EntityMapProfile : Profile
    {
        public EntityMapProfile() {
            CreateMap<AppUserDto, AppUser>().ReverseMap();
        }
    }
}
