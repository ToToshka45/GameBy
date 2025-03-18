using DataAccess.EntitiesRelations;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess;

public class DataContext : DbContext
{
    public DbSet<Event> Events { get; set; }


    //public DbSet<> Users { get; set; }
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
