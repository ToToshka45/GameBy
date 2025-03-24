using Application.Dto;
using AutoMapper;
using Domain;

namespace Application.MapperProfiles;

public class ApplicationMappingProfiles : Profile
{
    public ApplicationMappingProfiles()
    {
        CreateMap<CreateEventDto, Event>();
        CreateMap<Event, CreateEventDto>();
        CreateMap<Event, GetEventDto>();
        CreateMap<Event, GetShortEventDto>();

        CreateMap<AddParticipantDto, Participant>();
        CreateMap<Participant, GetParticipantDto>();
    }
}
