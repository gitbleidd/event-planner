namespace EventPlanner.App.Models.Account;

public record AuthResponse
{
    public required int Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}