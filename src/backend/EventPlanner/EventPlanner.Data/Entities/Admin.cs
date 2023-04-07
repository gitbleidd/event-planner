using System.ComponentModel.DataAnnotations;
using EventPlanner.Data.Entities;

namespace EventPlanner.Data;

public class Admin
{
    public int Id { get; set; }
    public required UserInfo UserInfo { get; set; }
    public required string Password { get; set; }
}