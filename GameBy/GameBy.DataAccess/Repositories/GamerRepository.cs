using GameBy.Core.Abstractions.Repositories;
using GameBy.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBy.DataAccess.Repositories;

public class GamerRepository : EfRepository<Gamer>, IGamerRepository
{
    public GamerRepository( ApplicationDBContext context ) : base( context )
    {
    }


}
