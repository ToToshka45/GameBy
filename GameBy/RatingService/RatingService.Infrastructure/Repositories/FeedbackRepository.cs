using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Primitives;
using RatingService.Infrastructure.Abstractions;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class FeedbackRepository(RatingServiceDbContext storage) : BaseRepository<Feedback>(storage) 
{
    // TODO: unique implementations while working with Feedbacks
}
