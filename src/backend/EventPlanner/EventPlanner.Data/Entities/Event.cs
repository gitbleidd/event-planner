using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Data.Entities;

public class Event
{
    public int Id { get; set; }
    public List<User> Users { get; } = new();
    
    public EventType? Type { get; set; }
    public required string Name { get; set; }
    public required string OrganizerName { get; set; }
    public required string LocationName { get; set; }
    public decimal? Cost { get; set; } // null - means free event
    public string Description { get; set; } = string.Empty;
    
    public required DateTimeOffset BeginTime { get; set; }
    public required DateTimeOffset EndTime { get; set; }
    public required DateTimeOffset RegistrationEndTime { get; set; }
    
    public int? Slots { get; set; } // null - unlimited count
    public int ExtraSlotsPerUser { get; set; } = 0;
    
    [Column(TypeName = "jsonb")] public string Resources { get; set; } = string.Empty;
}