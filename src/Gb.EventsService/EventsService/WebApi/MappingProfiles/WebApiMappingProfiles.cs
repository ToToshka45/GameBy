using Application.Dto;
using AutoMapper;
using WebApi.Dto;

namespace WebApi.MappingProfiles
{
    public class WebApiMappingProfiles:Profile
    {
        public WebApiMappingProfiles()
        {
            CreateMap<CreateEventRequest, CreateEventDto>();
            CreateMap<CreateEventDto, CreateEventResponse>();
            CreateMap<AddParticipantRequest, ParticipantAddDto>();
            CreateMap<ParticipantAddDto, AddParticipantResponse>();
            CreateMap<CreateEventDto, UpdateEventRequest> ();
            CreateMap<CreateEventResponse, CreateEventDto>();
            CreateMap<EventsFilters, EventsFiltersDto>();
        }
    }
}
