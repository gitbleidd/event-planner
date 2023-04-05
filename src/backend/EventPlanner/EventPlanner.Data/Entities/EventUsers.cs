namespace EventPlanner.Data.Entities;

public class EventUsers
{
    public int EventId { get; set; }
    public int UserId { get; set; }
    public EventInfo Event { get; set; } = null!;
    public UserInfo User { get; set; } = null!;
}