using AutoMapper;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;

namespace AchievementProfileService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности игрока.
    /// </summary>
    public class AchievementMappingsProfile : Profile
    {
        public AchievementMappingsProfile()
        {
            CreateMap<AchievementDto, AchievementModel>()
                .ForMember( d => d.Id, map => map.MapFrom( m => m.Id ) )
                .ForMember( d => d.AboutCondition, map => map.MapFrom( m => m.AboutCondition ) )
                .ForMember( d => d.AboutReward, map => map.MapFrom( m => m.AboutReward ) )
                //.ForMember( d => d.RankId, map => map.Ignore( m => m.RankId ) )
                .ForMember( d => d.Rank, map => map.MapFrom( m => new RankModel { Id = m.Rank.Id, Name = m.Rank.Name } ) );

            CreateMap<CreateAchievementModel, CreateAchievementDto>();
            CreateMap<UpdateAchievementModel, UpdateAchievementDto>();
        }
    }
}
