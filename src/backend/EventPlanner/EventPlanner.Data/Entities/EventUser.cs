namespace EventPlanner.Data.Entities;

public class EventUser
{
    public int EventId { get; set; }
    public int UserId { get; set; }
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
}