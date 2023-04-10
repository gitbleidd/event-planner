namespace EventPlanner.Data.Entities;

public class EventUser
{
    public int EventId { get; set; }
    public int UserId { get; set; }
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;

    public bool IsParticipating { get; set; }
    public int TakenExtraUsersCount { get; set; }
    public string Comment { get; set; } = string.Empty;
}