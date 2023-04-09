using EventPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data;

public sealed class EventPlannerContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    public DbSet<EventRegisteredUser> EventRegisteredUsers => Set<EventRegisteredUser>();
    public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();

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

        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<Event>().ToTable("event");
        modelBuilder.Entity<EventType>().ToTable("event_type");

        modelBuilder.Entity<User>()
            .HasMany(e => e.RegistredEvents)
            .WithMany(e => e.RegisteredUsers)
            .UsingEntity<EventRegisteredUser>("event_registered_user",
            l => l.HasOne<Event>(e => e.Event).WithMany(e => e.EventRegisteredUsers),
            r => r.HasOne<User>(e => e.User).WithMany(e => e.EventRegisteredUsers));
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.ParticipantEvents)
            .WithMany(e => e.Participants)
            .UsingEntity<EventParticipant>("event_participant",
                l => l.HasOne<Event>(e => e.Event).WithMany(e => e.EventParticipants),
                r => r.HasOne<User>(e => e.User).WithMany(e => e.EventParticipants));

        base.OnModelCreating(modelBuilder);
    }
}