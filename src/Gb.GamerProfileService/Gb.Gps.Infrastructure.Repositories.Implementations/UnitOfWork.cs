using Infrastructure.Repositories.Implementations;
using Services.Repositories.Abstractions;

namespace GameBy.DataAccess.Repositories;

/// <summary>
/// UOW.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly IGamerRepository _gamerRepository;

    private ApplicationDBContext _context;

    public IGamerRepository GamerRepository => _gamerRepository;

    public UnitOfWork( ApplicationDBContext context )
    {
        _context = context;

        _gamerRepository = new GamerRepository( context );
    }

    public async Task SaveChangesAsync( CancellationToken cancellationToken = default )
    {
        await _context.SaveChangesAsync( cancellationToken );
    }
}
