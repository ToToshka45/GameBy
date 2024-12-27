using Microsoft.Extensions.Logging;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Aggregates;
using RatingService.Domain.Entities;

namespace RatingService.Application.Services;

internal class RatingsProcessingService
{
    private readonly ILogger<RatingsProcessingService> _logger;
    private readonly IRepository<EventInfo> _eventRepo;
    private readonly IRepository<UserInfo> _userRepo;
    private readonly IRatingsRepository _ratingsRepo;

    public RatingsProcessingService(
        ILogger<RatingsProcessingService> logger,
        IRatingsRepository ratingsRepo,
        IRepository<EventInfo> eventRepo,
        IRepository<UserInfo> userRepo)
    {
        _logger = logger;
        _ratingsRepo = ratingsRepo;
        _eventRepo = eventRepo;
        _userRepo = userRepo;
    }

    public async Task AddRatingUpdate(RatingUpdate update, CancellationToken token)
    {
        await _ratingsRepo.AddAndRecalculate(update, token);
    }
}
