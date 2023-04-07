using System.ComponentModel.DataAnnotations;
using EventPlanner.Data.Entities;

namespace EventPlanner.Data;

public class Admin
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required string Password { get; set; }
}