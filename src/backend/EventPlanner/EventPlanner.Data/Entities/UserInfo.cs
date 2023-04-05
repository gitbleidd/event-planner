using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class UserInfo
{
    public int Id { get; set; }
    public List<EventInfo> Events { get; } = new();
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
}