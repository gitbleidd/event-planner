namespace EventPlanner.App.Authentication;

public class Tokens
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required string Jti { get; init; }
}