namespace EventPlanner.Data.Entities;

public class Admin
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required string Password { get; set; }
}