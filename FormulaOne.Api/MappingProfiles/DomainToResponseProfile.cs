using AutoMapper;
using FormulaOne.Entities;
using FormulaOne.Entities.Dtos.Responses;

namespace FormulaOne.Api.MappingProfiles;

public class DomainToResponseProfile : Profile
{
    public DomainToResponseProfile()
    {
        CreateMap<Achievement, DriverAchievementResponse>()
            .ForMember(dest => dest.Wins, opt => opt.MapFrom(src => src.RaceWins));
        
        CreateMap<Driver, GetDriverResponse>()
            .ForMember(dest => dest.Driverid, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}