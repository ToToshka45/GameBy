using AutoMapper;
using Domain.Entities;
using Gb.Gps.Services.Abstractions;
using Gb.Gps.Services.Contracts;
using Services.Repositories.Abstractions;

namespace Gb.Gps.Services.Implementations
{
    public class RankService : IRankService
    {
        private readonly IRankRepository _rankRepository;
        private readonly IMapper _mapper;

        public RankService( IRankRepository rankRepository, IMapper mapper )
        {
            _rankRepository = rankRepository;
            _mapper = mapper;
        }

        public async Task<List<RankDto>> GetAllAsync( CancellationToken cancellationToken )
        {
            var rank = await _rankRepository.GetAllAsync( cancellationToken );

            return _mapper.Map<List<Rank>, List<RankDto>>( rank );
        }

        public async Task<RankDto> GetByIdAsync( int id, CancellationToken cancellationToken )
        {
            var rank = await _rankRepository.GetAsync( id, cancellationToken );

            return _mapper.Map<Rank, RankDto>( rank );
        }

        public async Task<int> CreateAsync( CreateRankDto createRankDto, CancellationToken cancellationToken )
        {
            var rank = _mapper.Map<CreateRankDto, Rank>( createRankDto );
            var createdRank = await _rankRepository.AddAsync( rank, cancellationToken );

            await _rankRepository.SaveChangesAsync( cancellationToken );

            return createdRank.Id;
        }

        public async Task<bool> UpdateAsync( int id, UpdateRankDto updateRankDto, CancellationToken cancellationToken )
        {
            var rank = await _rankRepository.GetAsync( id, cancellationToken );

            if ( rank == null )
            {
                return false;
            }

            _mapper.Map<UpdateRankDto, Rank>( updateRankDto, rank );

            await _rankRepository.UpdateAsync( rank, cancellationToken );
            await _rankRepository.SaveChangesAsync( cancellationToken );

            return true;
        }

        public async Task<bool> DeleteAsync( int id, CancellationToken cancellationToken )
        {
            var wasDeleted = await _rankRepository.DeleteAsync( id, cancellationToken );

            if ( wasDeleted )
            {
                await _rankRepository.SaveChangesAsync( cancellationToken );
            }

            return wasDeleted;
        }
    }
}
