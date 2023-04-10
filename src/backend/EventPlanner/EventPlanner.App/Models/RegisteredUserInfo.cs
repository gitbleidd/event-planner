namespace EventPlanner.App.Models;

public record RegisteredUserInfo(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string Email,
    int TakenExtraUsersCount,
    string Comment);