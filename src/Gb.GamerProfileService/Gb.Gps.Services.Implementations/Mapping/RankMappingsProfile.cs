using AutoMapper;
using Domain.Entities;
using Gb.Gps.Services.Contracts;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности звания.
    /// </summary>
    public class RankMappingsProfile : Profile
    {
        public RankMappingsProfile()
        {
            CreateMap<Rank, RankDto>();

            CreateMap<CreateRankDto, Rank>()
                .ForMember( d => d.Id, map => map.Ignore() )
                .ForMember( d => d.Name, map => map.MapFrom( m => m.Name ) )
                .ForMember( d => d.Gamers, map => map.Ignore() );

            CreateMap<UpdateRankDto, Rank>()
                .ForMember( d => d.Id, map => map.Ignore() )
                .ForMember( d => d.Name, map => map.MapFrom( m => m.Name ) )
                .ForMember( d => d.Gamers, map => map.Ignore() );
        }
    }
}
