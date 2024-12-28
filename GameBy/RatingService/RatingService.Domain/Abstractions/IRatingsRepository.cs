using Microsoft.Extensions.Logging;
using RatingService.Domain.Entities;
using RatingService.Domain.Entities.Ratings;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Abstractions
{
    public interface IRatingsRepository
    {
        Task AddOrUpdate(RatingUpdate update, CancellationToken token);
    }
}
