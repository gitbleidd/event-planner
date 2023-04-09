namespace EventPlanner.Data.Entities;

public class EventRegisteredUser
{
    public int ExtraSlotsPerUser { get; set; }
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
}