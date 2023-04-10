namespace EventPlanner.Data.Entities;

public class EventRegisteredUser
{
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
    public int TakenExtraUsersCount { get; set; }
    public string Comment { get; set; } = string.Empty;
}