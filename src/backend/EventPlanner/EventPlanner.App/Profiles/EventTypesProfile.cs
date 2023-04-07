using AutoMapper;
using EventPlanner.App.Models;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Profiles;

public class EventTypesProfile : Profile
{
    public EventTypesProfile()
    {
        CreateMap<EventType, EventTypeInfo>();
        CreateMap<EventTypeInfo, EventType>();
    }
}