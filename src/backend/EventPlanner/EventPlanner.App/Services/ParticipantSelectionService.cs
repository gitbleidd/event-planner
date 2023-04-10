using EventPlanner.App.Models;
using EventPlanner.App.Services.Interfaces;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Services;

public class ParticipantSelectionService : IParticipantSelectionService
{
    public HashSet<User> GetParticipants(
        List<ParticipantSelectionModel> registeredUsers, 
        int? slots)
    {
        var seatsTaken = 0;
        slots ??= int.MaxValue;
        var participants = new List<ParticipantSelectionModel>();
        ChangeWeight(registeredUsers);

        while (registeredUsers.Any() && seatsTaken < slots)
        {
            var randomUser = GetRandomUser(registeredUsers);
            var countSeatsTakenByUser = randomUser.TakenExtraUsersCount + 1;

            if (seatsTaken + countSeatsTakenByUser <= slots)
            {
                participants.Add(randomUser);
                seatsTaken += countSeatsTakenByUser;
            }
            registeredUsers.Remove(randomUser);
        }
        
        return participants
            .Select(p => p.UserInfo)
            .ToHashSet();
    }


    private void ChangeWeight(List<ParticipantSelectionModel> users)
    {
        var totalWeight = users.Sum(u => u.Weight);
        users.ForEach(u => u.Weight = totalWeight - u.Weight);
    }

    private ParticipantSelectionModel GetRandomUser(
        List<ParticipantSelectionModel> users)
    {
        var random = new Random();
        var totalWeight = users.Sum(u => u.Weight);
        var randomNumber = random.Next(0, totalWeight + 1);
        return GetUserByNumber(users, randomNumber);
    }
    
    private ParticipantSelectionModel GetUserByNumber(
        List<ParticipantSelectionModel> users, 
        int randomNumber)
    {
        var currentSum = 0;
        foreach (var user in users)
        {
            currentSum += user.Weight;

            if (currentSum >= randomNumber)
                return user;
        }

        throw new InvalidOperationException();
    }
}