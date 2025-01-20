using AutoMapper;
using Domain.Entities;
using Infrastructure.DTOs;

namespace WebAPI.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(d => d.Age, o => o.MapFrom(s => CalculateAge(s.DateOfBirth)))
            .ForMember(d => d.PhotoUrl, o => 
                o.MapFrom(s => s.Photos.FirstOrDefault((x => x.IsMain))!.Url));
        CreateMap<Photo, PhotoDto>();
    }

    private static int CalculateAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
                
        var age = today.Year - birthDate.Year;
                
        if(birthDate > today.AddYears(-age)) age--;
                
        return age;
    }
}