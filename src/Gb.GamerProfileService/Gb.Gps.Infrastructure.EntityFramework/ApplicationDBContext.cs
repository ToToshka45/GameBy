using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GameBy.DataAccess;

public class ApplicationDBContext : DbContext
{
    /// <summary>
    /// Игроки.
    /// </summary>
    public DbSet<Gamer> Gamers { get; set; }

    /// <summary>
    /// Достижения.
    /// </summary>
    public DbSet<Achievement> Achievements { get; set; }

    /// <summary>
    /// Звания.
    /// </summary>
    public DbSet<Rank> Ranks { get; set; }

    public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options ) : base( options )
    {

    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        //modelBuilder.Entity<Gamer>()
        //            .HasOne<Rank>( s => s.Rank )
        //            .WithMany( g => g.Gamers )
        //            .HasForeignKey( s => s.RankId );

        modelBuilder.Entity<GamerAchievement>()
                    .HasKey( cp => new { cp.GamerId, cp.AchievementId } );
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.LogTo( Console.WriteLine, LogLevel.Information );
    }
}
