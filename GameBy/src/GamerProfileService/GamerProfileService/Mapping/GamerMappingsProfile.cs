using AutoMapper;
using GamerProfileService.Models.Gamer;
using Services.Contracts.Gamer;

namespace GamerProfileService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности игрока.
    /// </summary>
    public class GamerMappingsProfile : Profile
    {
        public GamerMappingsProfile()
        {
            CreateMap<GamerDto, GamerModel>();
            CreateMap<CreateGamerModel, CreateGamerDto>();
            CreateMap<UpdateGamerModel, UpdateGamerDto>();
        }
    }
}
