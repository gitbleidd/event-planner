using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Data.Entities;

public class EventType
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
}