using System.ComponentModel.DataAnnotations;

namespace EventPlanner.App.Models.Account;

public record RefreshTokenRequest
{
    [Required] public string AccessToken { get; init; } = string.Empty;

    [Required] public string RefreshToken { get; init; } = string.Empty;
}