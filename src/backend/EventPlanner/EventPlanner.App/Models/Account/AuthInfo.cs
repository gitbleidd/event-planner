using System.ComponentModel.DataAnnotations;

namespace EventPlanner.App.Models.Account;

public record AuthInfo
{
    [Required] [EmailAddress] public string Email { get; init; } = string.Empty;
    [Required] public string Password { get; init; } = string.Empty;
}