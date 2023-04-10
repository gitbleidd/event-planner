using EventPlanner.Data.Entities;

namespace EventPlanner.App.Models;

public class ParticipantSelectionModel
{
    public User UserInfo { get; set;}
    public int TakenExtraUsersCount { get; set;}
    public int Weight { get; set;}
    
    public ParticipantSelectionModel(
        User userInfo,
        int takenExtraUsersCount,
        int weight)
    {
        UserInfo = userInfo;
        TakenExtraUsersCount = takenExtraUsersCount;
        Weight = weight;
    }
}