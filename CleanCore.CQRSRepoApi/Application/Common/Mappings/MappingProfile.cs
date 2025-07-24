using Application.Common.DTOs.ResponseDtos;
using AutoMapper;
using Domain.Entities.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Example: map ApplicationUser -> AuthResponseDto
            CreateMap<ApplicationUser, AuthResponseDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                // Token, Expires, Roles set in handler
                ;
        }
    }
}