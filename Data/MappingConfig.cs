using System;
using seminar_API.Models;
using seminar_API.Models.DTOs;
using AutoMapper;

namespace seminar_API.Data
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Ticket, TicketDTO>().ReverseMap();
        }
    }
}
