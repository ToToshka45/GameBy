using Application.Dto;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapperProfiles
{
    public class ApplicationMappingProfiles : Profile
    {
        public ApplicationMappingProfiles() {
            CreateMap<EventDto, Event>();
            CreateMap<Event, EventDto>();
            CreateMap<PlayerAddDto,EventMember>();
            CreateMap<Event, ShortEventDto>();
        }
    }
}
