using Gb.Gps.Services.Contracts;

namespace Gb.Gps.Services.Abstractions
{
    public interface IAchievementService
    {
        /// <summary>
        /// Получить список достижений. 
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список ДТО достижений. </returns>
        Task<List<AchievementDto>> GetAllAsync( CancellationToken cancellationToken );

        /// <summary>
        /// Получить достижение. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО достижения. </returns>
        Task<AchievementDto> GetByIdAsync( int id, CancellationToken cancellationToken );

        /// <summary>
        /// Создать достижение.
        /// </summary>
        /// <param name="createAchievementDto"> ДТО достижения. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Идентификатор. </returns>
        Task<int> CreateAsync( CreateAchievementDto createAchievementDto, CancellationToken cancellationToken );

        /// <summary>
        /// Изменить достижение.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updateAchievementDto"> ДТО достижения. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> UpdateAsync( int id, UpdateAchievementDto updateAchievementDto, CancellationToken cancellationToken );

        /// <summary>
        /// Удалить достижение.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> DeleteAsync( int id, CancellationToken cancellationToken );
    }
}
