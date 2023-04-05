using System.ComponentModel.DataAnnotations;
using EventPlanner.Data.Entities;

namespace EventPlanner.Data;

public class Admin
{
    public int Id { get; set; }
    public UserInfo UserInfo { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}