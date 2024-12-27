using RatingService.Domain.Entities;
using RatingService.Domain.Primitives;

namespace RatingService.Domain.Abstractions
{
    public interface IRatingsRepository
    {
        Task AddAndRecalculate(RatingUpdate update, CancellationToken token);
        ValueTask<Rating?> GetRating(int subjectId, CancellationToken token);
    }
}
