using AutoMapper;
using StudentPortal.Models.Dtos.Login;
using StudentPortal.Models.Dtos.User;
using StudentPortal.Models.Models;

namespace StudenPortal.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
            CreateMap<Logins, LoginDto>().ReverseMap();
            CreateMap<UserProfile, UpdateProfileDto>().ReverseMap();
        }
    }

}