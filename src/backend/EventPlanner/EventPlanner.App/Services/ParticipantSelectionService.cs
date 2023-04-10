using EventPlanner.App.Services.Interfaces;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Services;

public class ParticipantSelectionService : IParticipantSelectionService
{
    public List<User> GetParticipants(
        Event eventInfo, 
        int? slots)
    {
        var seatsTaken = 0;
        slots ??= int.MaxValue;
        var participants = new List<User>();
        var registeredUsers = eventInfo.Users.ToList();

        while (registeredUsers.Any())
        {
            var randomUser = GetRandomUser(registeredUsers);
            var userCountSeatsTaken = eventInfo.EventUsers
                .Single(x => x.User == randomUser).TakenExtraUsersCount + 1;
            
            if (seatsTaken + userCountSeatsTaken <= slots)
            {
                participants.Add(randomUser);
                seatsTaken += userCountSeatsTaken;
            }
            registeredUsers.Remove(randomUser);
        }
        
        return participants;
    }

    private User GetRandomUser(List<User> users)
    {
        // TODO
        // var random = new Random();
        // var initialTotalWeight = users.Sum(u => u.ParticipantEvents.Count);
        // var updatedTotalWeight = users.Sum(u => initialTotalWeight - u.ParticipantEvents.Count);
        // var randomNumber = random.Next(0, updatedTotalWeight + 1);
        // return GetUserByNumber(users, randomNumber, initialTotalWeight);

        return users.First();
    }
    
    private User GetUserByNumber(
        List<User> sortedUsers, 
        int randomNumber, 
        int initialTotalWeight)
    {
        var currentSum = 0;
        foreach (var user in sortedUsers)
        {
            // TODO
            // currentSum += initialTotalWeight - user.ParticipantEvents.Count;

            if (currentSum >= randomNumber)
                return user;
        }

        throw new InvalidOperationException();
    }
}