using RatingService.Domain.Abstractions;
using RatingService.Domain.Models.Entities;
using RatingService.Infrastructure.Abstractions;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure.Repositories;

public class FeedbackRepository(RatingServiceDbContext storage) : BaseRepository<FeedbackInfo>(storage)
{
    // TODO: unique implementations while working with Feedbacks
}
