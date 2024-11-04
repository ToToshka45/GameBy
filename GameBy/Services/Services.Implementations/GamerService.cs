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

        public async Task<GamerDto> GetByIdAsync( int id, CancellationToken cancellationToken )
        {
            var gamer = await _gamerRepository.GetAsync( id, cancellationToken );

            if ( gamer == null )
            {
                throw new Exception( $"Игрок с id = {id} не найден" );
            }

            return _mapper.Map<Gamer, GamerDto>( gamer );
        }

        public async Task UpdateAsync( int id, UpdateGamerDto updateGamerDto )
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync( CreateGamerDto createGamerDto )
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync( int id )
        {
            throw new NotImplementedException();
        }
    }
}
