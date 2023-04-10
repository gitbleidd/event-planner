using System.ComponentModel.DataAnnotations;

namespace EventPlanner.App.Models;

public record EventSaveInfo
{
    [Required] public required int TypeId { get; init; }
    [Required] public required string Name { get; init; }
    [Required] public required string OrganizerName { get; init; }
    [Required] public required string LocationName { get; init; }
    
    [Range(1, 100_000)] public decimal? Cost { get; init; }
    [Required] public required string Description { get; init; }
    [Required] public required DateTimeOffset BeginTime { get; init; }
    [Required] public required DateTimeOffset EndTime { get; init; }
    [Required] public required DateTimeOffset RegistrationEndTime { get; init; }
    
    [Range(1, 100)] public int? Slots { get; init; }
    [Required] [Range(1, 100)] public required int ExtraSlotsPerUser { get; init; }
    [Required] public required string Resources { get; init; }
}