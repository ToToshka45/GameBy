using AutoMapper;
using GamerProfileService.Models;
using Gb.Gps.Services.Contracts;
using Gb.Gps.WebHost.Models;
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
            CreateMap<GamerDto, GamerModel>()
                .ForMember( d => d.Id, map => map.Ignore() )
                .ForMember( d => d.Name, map => map.MapFrom( m => m.Name ) )
                .ForMember( d => d.Nickname, map => map.MapFrom( m => m.Nickname ) )
                //.ForMember( d => d.DateOfBirth, map => map.MapFrom( m => m.DateOfBirth ) )
                //.ForMember( d => d.DateOfBirth, map => map.Ignore() )
                .ForMember( d => d.AboutMe, map => map.MapFrom( m => m.AboutMe ) )
                .ForMember( d => d.Country, map => map.MapFrom( m => m.Country ) )
                .ForMember( d => d.City, map => map.MapFrom( m => m.City ) )
                .ForMember( d => d.ContactMe, map => map.MapFrom( m => m.ContactMe ) )
                //.ForMember( d => d.GamerAchievements, map => map.Ignore() )
                .ForMember( d => d.RankId, map => map.MapFrom( m => m.RankId ) )
                .ForMember( d => d.Rank, map => map.MapFrom( m => new RankModel { Id = m.Rank.Id, Name = m.Rank.Name } ) );


            CreateMap<CreateGamerModel, CreateGamerDto>();
            CreateMap<UpdateGamerModel, UpdateGamerDto>();
        }
    }
}
