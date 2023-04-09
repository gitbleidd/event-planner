namespace EventPlanner.Data.Entities;

public class EventParticipant
{
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
}