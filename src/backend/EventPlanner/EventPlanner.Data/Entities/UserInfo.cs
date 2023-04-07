using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class UserInfo
{
    public int Id { get; set; }
    public List<EventInfo> Events { get; } = new();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string Email { get; set; }
}