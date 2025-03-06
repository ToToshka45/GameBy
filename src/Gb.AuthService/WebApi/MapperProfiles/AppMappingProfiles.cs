using Application.Dto;
using WebApi.Dto;
using AutoMapper;

namespace WebApi.MapperProfiles
{
    public class AppMappingProfiles : Profile
    {
        public AppMappingProfiles()
        {
            CreateMap<RegiserUserRequest, NewUserDto>();
        }
    }
}
