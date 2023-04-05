using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Data.Entities;

public class EventInfo
{
    public int Id { get; set; }
    public List<UserInfo> Users { get; } = new();
    
    public EventType? Type { get; set; }
    [Required] public string Name { get; set; } = null!;
    [Required] public string OrganizerName { get; set; } = null!;
    [Required] public string LocationName { get; set; } = null!;
    public decimal? Cost { get; set; } // null - means free event
    public string Description { get; set; } = string.Empty;
    
    [Required] public DateTimeOffset BeginTime { get; set; }
    [Required] public DateTimeOffset EndTime { get; set; }
    [Required] public DateTimeOffset RegistrationEndTime { get; set; }
    
    public int? Slots { get; set; } // null - unlimited count
    public int ExtraSlotsPerUser { get; set; } = 0;
    
    [Column(TypeName = "jsonb")] public string Resources { get; set; } = string.Empty;
}