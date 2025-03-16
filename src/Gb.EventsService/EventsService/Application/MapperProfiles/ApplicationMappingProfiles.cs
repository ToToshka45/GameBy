using Application.Dto;
using AutoMapper;
using Domain;
using WebApi.Dto;

namespace Application.MapperProfiles;

public class ApplicationMappingProfiles : Profile
{
    public ApplicationMappingProfiles() {
        CreateMap<CreateEventDto, Event>();
        CreateMap<Event, CreateEventDto>();
        CreateMap<ParticipantAddDto,Participant>();
        CreateMap<Event, GetShortEventDto>();
    }
}
