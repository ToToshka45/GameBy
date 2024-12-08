using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;
using RatingService.Infrastructure.Consts;
using System.Reflection;
using EventInfo = RatingService.Domain.Aggregates.EventInfo;

namespace RatingService.Infrastructure.DataAccess;

public class RatingServiceDbContext : DbContext
{
    public required DbSet<EventInfo> Events { get; set; }
    public required DbSet<UserInfo> Users { get; set; }
    public required DbSet<UserRatingUpdate> UsersRatingUpdates { get; set; }
    public required DbSet<EventRatingUpdate> EventsRatingUpdates { get; set; }

    public RatingServiceDbContext(DbContextOptions<RatingServiceDbContext> opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schemes.RatingServiceBaseSchema);
        // Model configuring
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
