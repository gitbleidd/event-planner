﻿namespace EventPlanner.App.Models;

public record UserInfo
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string Email { get; init; }
}