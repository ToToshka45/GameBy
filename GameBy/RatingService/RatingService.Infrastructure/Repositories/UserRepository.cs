using RatingService.Domain.Aggregates;
using RatingService.Infrastructure.Abstractions;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class UserRepository(RatingServiceDbContext storage) : BaseRepository<UserInfo>(storage)
{
    //public async Task
}
