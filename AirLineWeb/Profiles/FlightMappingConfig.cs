using AirLineWeb.Dtos;
using AirLineWeb.Models;
using AutoMapper;

namespace AirLineWeb.Profiles
{
    public class FlightMappingConfig:Profile
    {
        public FlightMappingConfig()
        {
            CreateMap<FlightDetailCreateDto,FlightDetail>().ReverseMap();
            CreateMap<FlightDetailReadDto,FlightDetail>().ReverseMap();
            CreateMap<FlightDetailUpdateDto,FlightDetail>().ReverseMap();
        }
    }
}
