using AutoMapper;
using EventPlanner.App.Models;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Profiles;

public class EventsProfile : Profile
{
    public EventsProfile()
    {
        CreateMap<Event, EventInfo>();
        CreateMap<EventSaveInfo, Event>();
    }
}