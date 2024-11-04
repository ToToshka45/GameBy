using Services.Contracts.Gamer;

namespace Services.Abstractions
{
    public interface IGamerService
    {
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
        Task<int> CreateAsync( CreateGamerDto createGamerDto );

        /// <summary>
        /// Изменить игрока.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updateGamerDto"> ДТО игрока. </param>
        Task UpdateAsync( int id, UpdateGamerDto updateGamerDto );

        /// <summary>
        /// Удалить игрока.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync( int id );
    }
}
