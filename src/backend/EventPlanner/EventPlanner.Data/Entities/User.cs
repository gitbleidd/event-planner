using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public List<Event> RegistredEvents { get; } = new();
    
    public List<Event> ParticipantEvents { get; } = new();
    public List<EventRegisteredUser> EventRegisteredUsers { get; } = new();
    
    public List<EventParticipant> EventParticipants { get; } = new();
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string Email { get; set; }
}