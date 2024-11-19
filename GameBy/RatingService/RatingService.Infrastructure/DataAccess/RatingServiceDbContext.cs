﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
using RatingService.Infrastructure.DataAccess.Configurations;

namespace RatingService.Infrastructure.DataAccess;

public class RatingServiceDbContext : DbContext
{
    public required DbSet<EventInfo> Events { get; set; }
    public required DbSet<UserInfo> Users { get; set; }
    public required DbSet<UserRatingUpdate> RatingUpdates { get; set; }

    public RatingServiceDbContext(DbContextOptions<RatingServiceDbContext> opts) : base(opts)
    {   
    }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Model configuring
        modelBuilder.ApplyConfiguration(new EventConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}
