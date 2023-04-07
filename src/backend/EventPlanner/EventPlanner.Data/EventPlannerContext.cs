﻿using EventPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data;

public sealed class EventPlannerContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Event> Events => Set<Event>();
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

        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<Event>().ToTable("event");
        modelBuilder.Entity<EventType>().ToTable("event_type");

        modelBuilder.Entity<User>()
            .HasMany(e => e.Events)
            .WithMany(e => e.Users)
            .UsingEntity<EventUser>("event_user",
            l => l.HasOne<Event>(e => e.Event).WithMany(e => e.EventUsers),
            r => r.HasOne<User>(e => e.User).WithMany(e => e.EventUsers));

        base.OnModelCreating(modelBuilder);
    }
}