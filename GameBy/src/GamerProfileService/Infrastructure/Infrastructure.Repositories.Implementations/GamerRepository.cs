using Domain.Entities;
using Services.Repositories.Abstractions;

namespace GameBy.DataAccess.Repositories;

public class GamerRepository : EfRepository<Gamer>, IGamerRepository
{
    public GamerRepository( ApplicationDBContext context ) : base( context )
    {
    }


}
