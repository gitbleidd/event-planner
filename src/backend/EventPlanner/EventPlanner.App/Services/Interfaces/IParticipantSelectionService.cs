using EventPlanner.App.Models;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Services.Interfaces;

public interface IParticipantSelectionService
{
    public HashSet<User> GetParticipants(
        List<ParticipantSelectionModel> registeredUsers, 
        int? slots);
}