using EventPlanner.App.Models;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Extensions;

public static class UserListExtensions
{
    public static List<RegisteredUserInfo> ToRegisteredUserInfo(
        this List<User> users,
        Event eventInfo)
    {
        return users
            .Select(u => new RegisteredUserInfo(
                u.Id,
                u.FirstName,
                u.LastName,
                u.MiddleName,
                u.Email,
                GetExtraSlotsForUser(u, eventInfo)
            ))
            .ToList();
    }
    
    private static int GetExtraSlotsForUser(
        User userInfo, 
        Event eventInfo)
    {
        return eventInfo.EventRegisteredUsers
            .Single(x => x.User == userInfo).ExtraSlotsPerUser;
    }
}
