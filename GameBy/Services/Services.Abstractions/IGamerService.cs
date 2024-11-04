using Services.Contracts.Gamer;

namespace Services.Abstractions
{
    public interface IGamerService
    {
        /// <summary>
        /// Получить список игроков. 
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список ДТО игроков. </returns>
        Task<List<GamerDto>> GetAllAsync( CancellationToken cancellationToken );

        /// <summary>
        /// Получить игрока. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО игрока. </returns>
        Task<GamerDto> GetByIdAsync( int id, CancellationToken cancellationToken );

        /// <summary>
        /// Создать игрока.
        /// </summary>
        /// <param name="createGamerDto"> ДТО игрока. </param>
        /// <returns> Идентификатор. </returns>
        Task<int> CreateAsync( CreateGamerDto createGamerDto, CancellationToken cancellationToken );

        /// <summary>
        /// Изменить игрока.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updateGamerDto"> ДТО игрока. </param>
        Task<bool> UpdateAsync( int id, UpdateGamerDto updateGamerDto, CancellationToken cancellationToken );

        /// <summary>
        /// Удалить игрока.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task<bool> DeleteAsync( int id, CancellationToken cancellationToken );
    }
}
