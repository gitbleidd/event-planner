using System.ComponentModel.DataAnnotations;

namespace EventPlanner.App.Models;

public record EventSaveInfo
{
    [Required]
    [Range(1, int.MaxValue)]
    public required int TypeId {get; init;}
    [Required]
    public required string Name {get; init;}
    [Required]
    public required string OrganizerName {get; init;}
    [Required]
    public required string LocationName {get; init;}
    
    [Range(1, int.MaxValue)]
    public decimal? Cost {get; init;}
    [Required]
    public required string Description {get; init;}
    [Required] 
    public required DateTimeOffset BeginTime {get; init;}
    [Required] 
    public required DateTimeOffset EndTime {get; init;}
    [Required] 
    public required DateTimeOffset RegistrationEndTime {get; init;}
    
    [Range(1, int.MaxValue)]
    public int? Slots {get; init;}
    
    [Required]
    [Range(1, int.MaxValue)]
    public required int ExtraSlotsPerUser {get; init;}
    [Required] 
    public required string Resources {get; init;}
}