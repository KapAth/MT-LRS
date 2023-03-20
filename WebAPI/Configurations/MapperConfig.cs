using AutoMapper;
using WebAPI.Models;
using WebAPI.Repositories.Repository.Entities;

namespace WebAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //map both ways
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserTitle, UserTitleDto>().ReverseMap();
            CreateMap<UserType, UserTypeDto>().ReverseMap();
        }
    }
}