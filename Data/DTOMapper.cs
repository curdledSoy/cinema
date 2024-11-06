using cinema.Data.DTO;
using AutoMapper;
using cinema.Models;

namespace cinema.Data;

public class DTOMapper: Profile
{
    public DTOMapper()
    {
        CreateMap<Cinema, CinemaDto>()
                .ForMember(dest => dest.TheaterCount, opt => opt.MapFrom(src => src.Theaters.Count));

        CreateMap<Theater, TheaterDto>();
    }
}