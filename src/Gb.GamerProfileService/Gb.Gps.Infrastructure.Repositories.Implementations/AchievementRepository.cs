using Domain.Entities;
using GameBy.DataAccess;
using GameBy.DataAccess.Repositories;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class AchievementRepository : EfRepository<Achievement>, IAchievementRepository
{
    public AchievementRepository( ApplicationDBContext context ) : base( context )
    {
    }


}
