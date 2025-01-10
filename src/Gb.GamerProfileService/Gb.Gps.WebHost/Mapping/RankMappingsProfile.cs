using AutoMapper;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;

namespace RankProfileService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности игрока.
    /// </summary>
    public class RankMappingsProfile : Profile
    {
        public RankMappingsProfile()
        {
            CreateMap<RankDto, RankModel>();
            CreateMap<CreateRankModel, CreateRankDto>();
            CreateMap<UpdateRankModel, UpdateRankDto>();
        }
    }
}
