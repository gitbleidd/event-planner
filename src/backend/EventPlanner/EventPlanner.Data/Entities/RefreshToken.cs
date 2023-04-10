using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Data.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string? JwtId { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}