using GameBy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameBy.DataAccess;

public class ApplicationDBContext : DbContext
{
    /// <summary>
    /// Игроки.
    /// </summary>
    public DbSet<Gamer> Gamers { get; set; }

    public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options ) : base( options )
    {

    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.LogTo( Console.WriteLine, LogLevel.Information );
    }
}
