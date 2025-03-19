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
            CreateMap<AddParticipantRequest, AddParticipantDto>();
            CreateMap<AddParticipantDto, AddParticipantResponse>();
            CreateMap<CreateEventDto, UpdateEventRequest> ();
            CreateMap<CreateEventResponse, CreateEventDto>();
            CreateMap<GetEventDto, GetEventResponse>();
            CreateMap<EventsFilters, EventsFiltersDto>();
            CreateMap<GetParticipantDto, GetParticipantResponse>();
        }
    }
}
