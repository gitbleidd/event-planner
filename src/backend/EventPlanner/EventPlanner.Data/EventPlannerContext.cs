using EventPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data;

public sealed class EventPlannerContext : DbContext
{
    public DbSet<UserInfo> Users => Set<UserInfo>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<EventInfo> Events => Set<EventInfo>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    
    public EventPlannerContext(DbContextOptions<EventPlannerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.SchemaName);
        
        modelBuilder.Entity<UserInfo>()
            .HasMany(e => e.Events)
            .WithMany(e => e.Users)
            .UsingEntity<EventUsers>();
        
        base.OnModelCreating(modelBuilder);
    }
}