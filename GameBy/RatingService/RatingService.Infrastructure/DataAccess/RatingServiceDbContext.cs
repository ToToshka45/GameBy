using Microsoft.EntityFrameworkCore;

namespace RatingService.Infrastructure.DataAccess;

public class RatingServiceDbContext : DbContext
{
    public RatingServiceDbContext(DbContextOptions<RatingServiceDbContext> opts) : base(opts)
    {   
    }
}
