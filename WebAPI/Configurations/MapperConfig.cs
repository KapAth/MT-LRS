using AutoMapper;
using Repositories.Repository.Entities;
using WebAPI.Models;

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