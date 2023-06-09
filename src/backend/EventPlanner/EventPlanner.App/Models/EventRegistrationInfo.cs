﻿using System.ComponentModel.DataAnnotations;

namespace EventPlanner.App.Models;

public record EventRegistrationInfo
{
    [Required] 
    [EmailAddress] 
    public required string UserEmail { get; init; }
    
    [Required] 
    public required int EventId { get; init; }
    
    [Required] 
    [Range(1, 100)]
    public required int TakenExtraUsersCount { get; init; }

    public string Comment { get; init; } = string.Empty;
}
    