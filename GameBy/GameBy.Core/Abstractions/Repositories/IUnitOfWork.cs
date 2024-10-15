namespace GameBy.Core.Abstractions.Repositories;

/// <summary>
/// UOW.
/// </summary>
public interface IUnitOfWork
{
    IGamerRepository GamerRepository { get; }

    Task SaveChangesAsync( CancellationToken cancellationToken = default );
}
