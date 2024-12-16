using Gb.Gps.Services.Contracts;
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
        /// <param name="cancellationToken"></param>
        /// <returns> Идентификатор. </returns>
        Task<int> CreateAsync( CreateGamerDto createGamerDto, CancellationToken cancellationToken );

        /// <summary>
        /// Изменить игрока.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updateGamerDto"> ДТО игрока. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> UpdateAsync( int id, UpdateGamerDto updateGamerDto, CancellationToken cancellationToken );

        /// <summary>
        /// Удалить игрока.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> DeleteAsync( int id, CancellationToken cancellationToken );

        /// <summary>
        /// Установить звание игроку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="setGamerRankDto"> ДТО установки звания. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> SetRankAsync( int id, SetGamerRankDto setGamerRankDto, CancellationToken cancellationToken );

        /// <summary>
        /// Выдать достижение игроку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="giveAchievementToGamerDto"> ДТО выдачи достижения. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> GiveAchievementAsync( int id, GiveAchievementToGamerDto giveAchievementToGamerDto, CancellationToken cancellationToken );

        /// <summary>
        /// Получить достижения игрока. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список достижений. </returns>
        Task<List<AchievementDto>> GetEarnedAchievementsByIdAsync( int id, CancellationToken cancellationToken );

        /// <summary>
        /// Получить доступные игроку звания. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список званий. </returns>
        Task<List<RankDto>> GetAvailableRanksByIdAsync( int id, CancellationToken cancellationToken );
    }
}
