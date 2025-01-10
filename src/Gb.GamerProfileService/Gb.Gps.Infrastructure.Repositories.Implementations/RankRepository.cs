using Domain.Entities;
using GameBy.DataAccess;
using GameBy.DataAccess.Repositories;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class RankRepository : EfRepository<Rank>, IRankRepository
{
    public RankRepository( ApplicationDBContext context ) : base( context )
    {
    }


}
