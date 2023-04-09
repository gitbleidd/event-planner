using AutoMapper;
using EventPlanner.App.Models;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Profiles;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserInfo>();
    }
}