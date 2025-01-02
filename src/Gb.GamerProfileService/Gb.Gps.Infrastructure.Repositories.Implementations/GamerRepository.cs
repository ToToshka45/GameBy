using Domain.Entities;
using GameBy.DataAccess;
using GameBy.DataAccess.Repositories;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class GamerRepository : EfRepository<Gamer>, IGamerRepository
{
    public GamerRepository( ApplicationDBContext context ) : base( context )
    {
    }


}
