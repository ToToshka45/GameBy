using AutoMapper;
using Domain.Entities;
using Gb.Gps.Services.Contracts;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности достижения.
    /// </summary>
    public class AchievementMappingsProfile : Profile
    {
        public AchievementMappingsProfile()
        {
            CreateMap<Achievement, AchievementDto>();

            CreateMap<CreateAchievementDto, Achievement>()
                .ForMember( d => d.Id, map => map.Ignore() )
                .ForMember( d => d.AboutCondition, map => map.MapFrom( m => m.AboutCondition ) )
                .ForMember( d => d.AboutReward, map => map.MapFrom( m => m.AboutReward ) )
                .ForMember( d => d.RankId, map => map.MapFrom( m => m.RankId ) )
                .ForMember( d => d.Rank, map => map.Ignore() )
                .ForMember( d => d.GamerAchievements, map => map.Ignore() );

            CreateMap<UpdateAchievementDto, Achievement>()
                .ForMember( d => d.Id, map => map.Ignore() )
                .ForMember( d => d.AboutCondition, map => map.MapFrom( m => m.AboutCondition ) )
                .ForMember( d => d.AboutReward, map => map.MapFrom( m => m.AboutReward ) )
                .ForMember( d => d.RankId, map => map.MapFrom( m => m.RankId ) )
                .ForMember( d => d.Rank, map => map.Ignore() )
                .ForMember( d => d.GamerAchievements, map => map.Ignore() );
        }
    }
}
