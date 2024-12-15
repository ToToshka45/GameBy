using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.Gamer;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{
    public class GamerService : IGamerService
    {
        private readonly IGamerRepository _gamerRepository;
        private readonly IMapper _mapper;

        public GamerService( IGamerRepository gamerRepository, IMapper mapper )
        {
            _gamerRepository = gamerRepository;
            _mapper = mapper;
        }

        public async Task<List<GamerDto>> GetAllAsync( CancellationToken cancellationToken )
        {
            var gamer = await _gamerRepository.GetAllAsync( cancellationToken );

            return _mapper.Map<List<Gamer>, List<GamerDto>>( gamer );
        }
        
        public async Task<GamerDto> GetByIdAsync( int id, CancellationToken cancellationToken )
        {
            var gamer = await _gamerRepository.GetAsync( id, cancellationToken );

            return _mapper.Map<Gamer, GamerDto>( gamer );
        }

        public async Task<int> CreateAsync( CreateGamerDto createGamerDto, CancellationToken cancellationToken )
        {
            var gamer = _mapper.Map<CreateGamerDto, Gamer>( createGamerDto );
            var createdGamer = await _gamerRepository.AddAsync( gamer, cancellationToken );

            await _gamerRepository.SaveChangesAsync( cancellationToken );

            return createdGamer.Id;
        }

        public async Task<bool> UpdateAsync( int id, UpdateGamerDto updateGamerDto, CancellationToken cancellationToken )
        {
            var gamer = await _gamerRepository.GetAsync( id, cancellationToken );

            if ( gamer == null )
            {
                return false;
            }

            _mapper.Map<UpdateGamerDto, Gamer>( updateGamerDto, gamer );

            await _gamerRepository.UpdateAsync( gamer, cancellationToken );
            await _gamerRepository.SaveChangesAsync( cancellationToken );

            return true;
        }

        public async Task<bool> DeleteAsync( int id, CancellationToken cancellationToken )
        {
            var wasDeleted = await _gamerRepository.DeleteAsync( id, cancellationToken );

            if ( wasDeleted )
            {
                await _gamerRepository.SaveChangesAsync( cancellationToken );
            }

            return wasDeleted;
        }

        public async Task<bool> SetRankAsync( int id, SetGamerRankDto setGamerRankDto, CancellationToken cancellationToken )
        {
            var gamer = await _gamerRepository.GetAsync( id, cancellationToken );

            if ( gamer == null )
            {
                return false;
            }

            gamer.RankId = setGamerRankDto.RankId;

            await _gamerRepository.UpdateAsync( gamer, cancellationToken );
            await _gamerRepository.SaveChangesAsync( cancellationToken );

            return true;
        }
    }
}
