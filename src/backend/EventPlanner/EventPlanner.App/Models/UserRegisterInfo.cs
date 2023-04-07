using System.ComponentModel.DataAnnotations;

namespace EventPlanner.App.Models;

public record UserRegisterInfo
{
    [Required] public required string FirstName { get; init; }
    [Required] public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    
    [Required] 
    [EmailAddress] 
    public required string Email { get; init; }
}