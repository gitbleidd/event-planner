using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Data.Entities;

public class EventType
{
    public int Id { get; set; }
    public required string Name { get; set; }
}