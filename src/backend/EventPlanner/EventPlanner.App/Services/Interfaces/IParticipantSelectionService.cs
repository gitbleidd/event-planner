using EventPlanner.App.Models;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Services.Interfaces;

public interface IParticipantSelectionService
{
    public List<User> GetParticipants(
        Event eventInfo, 
        int? slots);
}