using AutoMapper;
using Domain.Entities;
using Services.Contracts.Gamer;

namespace Services.Implementations.Mapping
{
    public class GamerMappingsProfile : Profile
    {
        public GamerMappingsProfile()
        {
            CreateMap<Gamer, GamerDto>();
        }
    }
}
