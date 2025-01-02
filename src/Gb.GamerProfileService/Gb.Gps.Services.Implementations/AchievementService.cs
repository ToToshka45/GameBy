using AutoMapper;
using Domain.Entities;
using Gb.Gps.Services.Abstractions;
using Gb.Gps.Services.Contracts;
using Services.Repositories.Abstractions;

namespace Gb.Gps.Services.Implementations
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public AchievementService( IAchievementRepository achievementRepository, IMapper mapper )
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<List<AchievementDto>> GetAllAsync( CancellationToken cancellationToken )
        {
            var achievement = await _achievementRepository.GetAllAsync( cancellationToken );

            return _mapper.Map<List<Achievement>, List<AchievementDto>>( achievement );
        }

        public async Task<AchievementDto> GetByIdAsync( int id, CancellationToken cancellationToken )
        {
            var achievement = await _achievementRepository.GetAsync( id, cancellationToken );

            return _mapper.Map<Achievement, AchievementDto>( achievement );
        }

        public async Task<int> CreateAsync( CreateAchievementDto createAchievementDto, CancellationToken cancellationToken )
        {
            var achievement = _mapper.Map<CreateAchievementDto, Achievement>( createAchievementDto );
            var createdAchievement = await _achievementRepository.AddAsync( achievement, cancellationToken );

            await _achievementRepository.SaveChangesAsync( cancellationToken );

            return createdAchievement.Id;
        }

        public async Task<bool> UpdateAsync( int id, UpdateAchievementDto updateAchievementDto, CancellationToken cancellationToken )
        {
            var achievement = await _achievementRepository.GetAsync( id, cancellationToken );

            if ( achievement == null )
            {
                return false;
            }

            _mapper.Map<UpdateAchievementDto, Achievement>( updateAchievementDto, achievement );

            await _achievementRepository.UpdateAsync( achievement, cancellationToken );
            await _achievementRepository.SaveChangesAsync( cancellationToken );

            return true;
        }

        public async Task<bool> DeleteAsync( int id, CancellationToken cancellationToken )
        {
            var wasDeleted = await _achievementRepository.DeleteAsync( id, cancellationToken );

            if ( wasDeleted )
            {
                await _achievementRepository.SaveChangesAsync( cancellationToken );
            }

            return wasDeleted;
        }
    }
}
