using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Models.Entities;
using RatingService.Domain.Models.ValueObjects;
using System.Reflection;

namespace RatingService.Infrastructure.DataAccess;

public class RatingServiceDbContext : DbContext
{
    public DbSet<ParticipantInfo> Participants { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<UserInfo> UsersInfo { get; set; }
    //public DbSet<Participant> Participants { get; set; }
    //public DbSet<Participant> Participants { get; set; }

    public RatingServiceDbContext(DbContextOptions<RatingServiceDbContext> opts) : base(opts)
    {   
    }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Model configuring
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // TODO
    }
}
