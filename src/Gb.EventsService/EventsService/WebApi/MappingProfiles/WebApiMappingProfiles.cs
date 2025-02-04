using Application.Dto;
using AutoMapper;
using Domain;
using WebApi.Dto;

namespace WebApi.MappingProfiles
{
    public class WebApiMappingProfiles:Profile
    {
        public WebApiMappingProfiles()
        {
            CreateMap<NewEventRequest, EventDto>();
            CreateMap<EventDto, NewEventResponse>();
            CreateMap<PlayerAddRequest, PlayerAddDto>();
            CreateMap<PlayerAddDto, PlayerAddedResponse>();
            CreateMap<EventDto, UpdateEventRequest> ();
            CreateMap<NewEventResponse, EventDto>();
            CreateMap<EventsFilter,EventsFilterDto>();
        }
    }
}
