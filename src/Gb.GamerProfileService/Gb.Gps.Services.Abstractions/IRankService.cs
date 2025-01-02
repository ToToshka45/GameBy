using Gb.Gps.Services.Contracts;
using Services.Contracts.Gamer;

namespace Gb.Gps.Services.Abstractions
{
    public interface IRankService
    {
        /// <summary>
        /// Получить список званий. 
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список ДТО званий. </returns>
        Task<List<RankDto>> GetAllAsync( CancellationToken cancellationToken );

        /// <summary>
        /// Получить звание. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО звания. </returns>
        Task<RankDto> GetByIdAsync( int id, CancellationToken cancellationToken );

        /// <summary>
        /// Создать звание.
        /// </summary>
        /// <param name="createRankDto"> ДТО звания. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Идентификатор. </returns>
        Task<int> CreateAsync( CreateRankDto createRankDto, CancellationToken cancellationToken );

        /// <summary>
        /// Изменить звание.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updateRankDto"> ДТО звания. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> UpdateAsync( int id, UpdateRankDto updateRankDto, CancellationToken cancellationToken );

        /// <summary>
        /// Удалить звание.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"></param>
        Task<bool> DeleteAsync( int id, CancellationToken cancellationToken );
    }
}
