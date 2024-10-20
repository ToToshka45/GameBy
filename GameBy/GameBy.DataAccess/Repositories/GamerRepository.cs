using GameBy.Core.Abstractions.Repositories;
using GameBy.Core.Domain.Entities;

namespace GameBy.DataAccess.Repositories;

public class GamerRepository : EfRepository<Gamer>, IGamerRepository
{
    public GamerRepository( ApplicationDBContext context ) : base( context )
    {
    }


}
